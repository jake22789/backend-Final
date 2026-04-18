using System.Data;
using final_proj.DTO;
using final_proj.Models;
using final_proj.Services.Exceptions;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace final_proj.Services;

public interface IProcurementService
{
    Task<PurchaseOrderResponseDto> CreatePurchaseOrderAsync(CreatePurchaseOrderRequestDto dto);

    Task<ShipmentResponseDto> CreateShipmentAsync(CreateShipmentRequestDto dto);

    Task<ReceiveShipmentResponseDto> ReceiveShipmentAsync(int shipmentId);
}

public class ProcurementService : IProcurementService
{
    private readonly Db26TeamoneContext _context;

    public ProcurementService(Db26TeamoneContext context)
    {
        _context = context;
    }

    public async Task<PurchaseOrderResponseDto> CreatePurchaseOrderAsync(CreatePurchaseOrderRequestDto dto)
    {
        if (dto.Items.Count == 0)
        {
            throw new BadRequestApiException("A purchase order must contain one or more items.");
        }

        var vendorExists = await _context.Vendors1.AnyAsync(v => v.Id == dto.VendorId);
        if (!vendorExists)
        {
            throw new NotFoundApiException("Vendor not found.");
        }

        var requestedItemIds = dto.Items.Select(i => i.ItemId).Distinct().ToList();
        var existingItemIds = await _context.Items1
            .Where(i => requestedItemIds.Contains(i.Id))
            .Select(i => i.Id)
            .ToListAsync();

        var missingItemId = requestedItemIds.FirstOrDefault(id => !existingItemIds.Contains(id));
        if (missingItemId != 0)
        {
            throw new NotFoundApiException($"Item {missingItemId} not found.");
        }

        var purchaseOrder = new PurchaseOrder1
        {
            VendorId = dto.VendorId,
            DateOrdered = DateTime.UtcNow,
            PurchaseOrderItems = dto.Items.Select(i => new PurchaseOrderItem
            {
                ItemId = i.ItemId,
                Quantity = i.Quantity,
                Price = i.UnitPrice
            }).ToList()
        };

        _context.PurchaseOrders1.Add(purchaseOrder);
        await _context.SaveChangesAsync();

        return new PurchaseOrderResponseDto
        {
            Id = purchaseOrder.Id,
            VendorId = purchaseOrder.VendorId ?? dto.VendorId,
            DateOrdered = purchaseOrder.DateOrdered,
            Items = dto.Items
        };
    }

    public async Task<ShipmentResponseDto> CreateShipmentAsync(CreateShipmentRequestDto dto)
    {
        if (dto.Items.Count == 0)
        {
            throw new BadRequestApiException("A shipment must contain one or more items.");
        }

        var purchaseOrder = await _context.PurchaseOrders1
            .Include(o => o.PurchaseOrderItems)
            .FirstOrDefaultAsync(o => o.Id == dto.PurchaseOrderId);

        if (purchaseOrder == null)
        {
            throw new NotFoundApiException("Purchase order not found.");
        }

        var poItemIds = purchaseOrder.PurchaseOrderItems
            .Where(i => i.ItemId.HasValue)
            .Select(i => i.ItemId!.Value)
            .ToHashSet();

        var invalidItem = dto.Items.FirstOrDefault(i => !poItemIds.Contains(i.ItemId));
        if (invalidItem != null)
        {
            throw new ConflictApiException($"Item {invalidItem.ItemId} is not part of purchase order {dto.PurchaseOrderId}.");
        }

        var shipment = new Shipment
        {
            OrderId = dto.PurchaseOrderId,
            HandlingCost = dto.HandlingCost,
            DateReceived = null,
            ItemShipments = dto.Items.Select(i => new ItemShipment
            {
                ItemId = i.ItemId,
                Quantity = i.Quantity,
                UnitPrice = i.UnitPrice
            }).ToList()
        };

        _context.Shipments.Add(shipment);
        await _context.SaveChangesAsync();

        return new ShipmentResponseDto
        {
            Id = shipment.Id,
            PurchaseOrderId = shipment.OrderId ?? dto.PurchaseOrderId,
            DateReceived = shipment.DateReceived,
            HandlingCost = shipment.HandlingCost ?? dto.HandlingCost,
            IsReceived = shipment.DateReceived.HasValue,
            Items = dto.Items
        };
    }

    public async Task<ReceiveShipmentResponseDto> ReceiveShipmentAsync(int shipmentId)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync(IsolationLevel.Serializable);

        var shipment = await _context.Shipments
            .Include(s => s.ItemShipments)
            .FirstOrDefaultAsync(s => s.Id == shipmentId);

        if (shipment == null)
        {
            throw new NotFoundApiException("Shipment not found.");
        }

        if (shipment.DateReceived.HasValue)
        {
            await transaction.CommitAsync();
            return new ReceiveShipmentResponseDto
            {
                ShipmentId = shipment.Id,
                AlreadyReceived = true,
                ReceivedAtUtc = DateTime.SpecifyKind(shipment.DateReceived.Value, DateTimeKind.Utc)
            };
        }

        shipment.DateReceived = DateTime.UtcNow;

        foreach (var itemShipment in shipment.ItemShipments)
        {
            if (!itemShipment.ItemId.HasValue || !itemShipment.Quantity.HasValue || itemShipment.Quantity.Value <= 0)
            {
                throw new ConflictApiException("Shipment line item is invalid.");
            }

            await AddToInventoryForReceivedShipmentAsync(itemShipment.ItemId.Value, itemShipment.Quantity.Value);
        }

        try
        {
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch (DbUpdateException ex) when (ex.InnerException is PostgresException pg && pg.SqlState == PostgresErrorCodes.UniqueViolation)
        {
            throw new ConflictApiException("Concurrent update conflict while receiving shipment.");
        }

        return new ReceiveShipmentResponseDto
        {
            ShipmentId = shipment.Id,
            AlreadyReceived = false,
            ReceivedAtUtc = shipment.DateReceived.Value
        };
    }

    private async Task AddToInventoryForReceivedShipmentAsync(int itemId, int quantity)
    {
        var inventory = await _context.InventoryRecords
            .Where(i => i.ItemId == itemId)
            .OrderBy(i => i.Id)
            .FirstOrDefaultAsync();

        if (inventory != null)
        {
            inventory.Quantity += quantity;
            inventory.UpdatedAt = DateTime.UtcNow;
            return;
        }

        var availableBin = await _context.Bins1
            .Where(b => !_context.InventoryRecords.Any(i => i.BinId == b.Id))
            .OrderBy(b => b.Id)
            .Select(b => (int?)b.Id)
            .FirstOrDefaultAsync();

        if (!availableBin.HasValue)
        {
            throw new ConflictApiException("No empty bin is available for received inventory.");
        }

        _context.InventoryRecords.Add(new InventoryRecord
        {
            BinId = availableBin.Value,
            ItemId = itemId,
            Quantity = quantity,
            UpdatedAt = DateTime.UtcNow
        });
    }
}

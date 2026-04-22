using System.Data;
using final_proj.DTO;
using final_proj.Models;
using final_proj.Services.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace final_proj.Services;

public interface IInventoryService
{
    Task<InventoryResponseDto> StoreInventoryAsync(StoreInventoryRequestDto dto);

    Task<InventoryReportResponseDto> GetInventoryReportAsync();

    Task<InventoryDetailedReportResponseDto> GetInventoryByProductAsync(int productId);

    Task<List<InventoryOnHandDto>> GetInventoryOnHandAsync();

    Task<List<ItemsNeedingToBeShippedDto>> GetItemsNeedingToBeShippedAsync();

    Task<List<OrdersNeedingToBeShippedDto>> GetOrdersNeedingToBeShippedAsync();

    Task<List<ProfitabilityPerItemDto>> GetProfitabilityPerItemAsync();
}

public class InventoryService : IInventoryService
{
    private readonly Db26TeamoneContext _context;

    public InventoryService(Db26TeamoneContext context)
    {
        _context = context;
    }

    public async Task<InventoryResponseDto> StoreInventoryAsync(StoreInventoryRequestDto dto)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync(IsolationLevel.Serializable);

        var itemExists = await _context.Items1.AnyAsync(i => i.Id == dto.ItemId);
        if (!itemExists)
        {
            throw new NotFoundApiException("Item not found.");
        }

        var binExists = await _context.Bins1.AnyAsync(b => b.Id == dto.BinId);
        if (!binExists)
        {
            throw new NotFoundApiException("Bin not found.");
        }

        var binConflict = await _context.InventoryRecords
            .AnyAsync(i => i.BinId == dto.BinId && i.ItemId != dto.ItemId);

        if (binConflict)
        {
            throw new ConflictApiException("A bin stores only one product.");
        }

        var inventory = await _context.InventoryRecords
            .FirstOrDefaultAsync(i => i.BinId == dto.BinId && i.ItemId == dto.ItemId);

        if (inventory == null)
        {
            throw new NotFoundApiException("Inventory must exist before it can be stored.");
        }

        var rowsUpdated = await _context.Database.ExecuteSqlInterpolatedAsync(
            $"UPDATE warehouse.app_inventory SET quantity = quantity + {dto.Quantity}, updated_at = now() WHERE id = {inventory.Id}"
        );

        if (rowsUpdated == 0)
        {
            throw new ConflictApiException("Inventory update failed due to concurrent modification.");
        }

        await transaction.CommitAsync();

        var refreshed = await _context.InventoryRecords.FirstAsync(i => i.Id == inventory.Id);
        return new InventoryResponseDto
        {
            Id = refreshed.Id,
            ItemId = refreshed.ItemId,
            BinId = refreshed.BinId,
            Quantity = refreshed.Quantity
        };
    }

    public async Task<InventoryReportResponseDto> GetInventoryReportAsync()
    {
        var inventoryByItem = await _context.InventoryRecords
            .Include(i => i.Item)
            .GroupBy(i => i.ItemId)
            .Select(g => new InventoryItemDto
            {
                ItemId = g.Key,
                ItemName = g.First().Item!.ItemName,
                Price = (int?)(g.First().Item!.Price ?? 0),
                Volume = g.First().Item!.ItemSize,
                TotalQuantityAvailable = g.Sum(i => i.Quantity)
            })
            .OrderBy(i => i.ItemId)
            .ToListAsync();

        var totalQuantity = inventoryByItem.Sum(i => i.TotalQuantityAvailable);

        return new InventoryReportResponseDto
        {
            Items = inventoryByItem,
            TotalUniqueItems = inventoryByItem.Count,
            TotalQuantity = totalQuantity
        };
    }

    public async Task<InventoryDetailedReportResponseDto> GetInventoryByProductAsync(int productId)
    {
        var itemExists = await _context.Items1.AnyAsync(i => i.Id == productId);
        if (!itemExists)
        {
            throw new NotFoundApiException($"Product with ID {productId} not found.");
        }

        var locations = await _context.InventoryRecords
            .Where(i => i.ItemId == productId)
            .Include(i => i.Item)
            .Select(i => new InventoryLocationDto
            {
                InventoryRecordId = i.Id,
                ItemId = i.ItemId,
                ItemName = i.Item!.ItemName,
                BinId = i.BinId,
                Quantity = i.Quantity,
                UpdatedAt = i.UpdatedAt
            })
            .OrderBy(i => i.BinId)
            .ToListAsync();

        var totalQuantity = locations.Sum(l => l.Quantity);

        return new InventoryDetailedReportResponseDto
        {
            Locations = locations,
            TotalLocations = locations.Count,
            TotalQuantity = totalQuantity
        };
    }

    public async Task<List<InventoryOnHandDto>> GetInventoryOnHandAsync()
    {
        return await _context.InventoryOnHands
            .Select(i => new InventoryOnHandDto
            {
                Id = i.Id,
                ItemName = i.ItemName,
                QtyInWarehouse = i.QtyInWarehouse,
                QtySoldNotYetShipped = i.QtySoldNotYetShipped,
                QtyAvailableForSale = i.QtyAvailableForSale
            })
            .OrderBy(i => i.ItemName)
            .ToListAsync();
    }

    public async Task<List<ItemsNeedingToBeShippedDto>> GetItemsNeedingToBeShippedAsync()
    {
        return await _context.ItemsNeedingToBeShippeds
            .Select(i => new ItemsNeedingToBeShippedDto
            {
                OrderId = i.OrderId,
                OrderDate = i.OrderDate,
                ItemId = i.ItemId,
                QtyOrdered = i.QtyOrdered,
                QtyAlreadyShipped = i.QtyAlreadyShipped,
                QtyRemainingToBeShipped = i.QtyRemainingToBeShipped,
                QuantityInWarehouse = i.QuantityInWarehouse
            })
            .OrderBy(i => i.OrderId)
            .ToListAsync();
    }

    public async Task<List<OrdersNeedingToBeShippedDto>> GetOrdersNeedingToBeShippedAsync()
    {
        return await _context.OrdersNeedingToBeShippeds
            .Select(o => new OrdersNeedingToBeShippedDto
            {
                OrderId = o.OrderId,
                OrderDate = o.OrderDate,
                ItemId = o.ItemId,
                QtyOrdered = o.QtyOrdered,
                QtyAlreadyShipped = o.QtyAlreadyShipped,
                QtyRemainingToBeShipped = o.QtyRemainingToBeShipped
            })
            .OrderBy(o => o.OrderId)
            .ToListAsync();
    }

    public async Task<List<ProfitabilityPerItemDto>> GetProfitabilityPerItemAsync()
    {
        return await _context.ProfitabilityPerItems
            .Select(p => new ProfitabilityPerItemDto
            {
                ItemId = p.ItemId,
                Name = p.Name,
                QtySold = p.QtySold,
                AvgCostToPurchase = p.AvgCostToPurchase,
                AvgRevenuePerSale = p.AvgRevenuePerSale,
                ProfitPerUnit = p.ProfitPerUnit
            })
            .OrderBy(p => p.ItemId)
            .ToListAsync();
    }
}

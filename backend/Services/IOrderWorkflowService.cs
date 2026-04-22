using System.Data;
using final_proj.DTO;
using final_proj.Models;
using final_proj.Services.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace final_proj.Services;

public interface IOrderWorkflowService
{
    Task<OrderResponseDto> CreateOrderAsync(CreateCustomerOrderRequestDto dto);

    Task<OrderStateChangeResponseDto> PickOrderAsync(int orderId);

    Task<OrderStateChangeResponseDto> PackOrderAsync(int orderId);

    Task<OrderStateChangeResponseDto> ShipOrderAsync(int orderId);

    Task<OrderStatusDto> GetOrderStatusAsync(int orderId);
}

public class OrderWorkflowService : IOrderWorkflowService
{
    private readonly Db26TeamoneContext _context;

    public OrderWorkflowService(Db26TeamoneContext context)
    {
        _context = context;
    }

    public async Task<OrderResponseDto> CreateOrderAsync(CreateCustomerOrderRequestDto dto)
    {
        if (dto.Items.Count == 0)
        {
            throw new BadRequestApiException("Orders must contain one or more items.");
        }

        var customerExists = await _context.Customers.AnyAsync(c => c.Id == dto.CustomerId);
        if (!customerExists)
        {
            throw new NotFoundApiException("Customer not found.");
        }

        var itemIds = dto.Items.Select(i => i.ItemId).Distinct().ToList();
        var existingItemIds = await _context.Items1
            .Where(i => itemIds.Contains(i.Id))
            .Select(i => i.Id)
            .ToListAsync();

        var missingItem = itemIds.FirstOrDefault(i => !existingItemIds.Contains(i));
        if (missingItem != 0)
        {
            throw new NotFoundApiException($"Item {missingItem} not found.");
        }

        var order = new CustomerOrder
        {
            CustomerId = dto.CustomerId,
            ShippingFee = dto.ShippingFee,
            Date = DateTime.UtcNow,
            CustomerOrderToItems = dto.Items.Select(i => new CustomerOrderToItem
            {
                ItemId = i.ItemId,
                Qty = i.Quantity,
                Price = i.UnitPrice
            }).ToList()
        };

        _context.CustomerOrders.Add(order);
        await _context.SaveChangesAsync();

        _context.OrderWorkflowStates.Add(new OrderWorkflowState
        {
            OrderId = order.Id,
            Status = "CREATED",
            UpdatedAt = DateTime.UtcNow
        });

        await _context.SaveChangesAsync();

        return new OrderResponseDto
        {
            Id = order.Id,
            CustomerId = order.CustomerId ?? dto.CustomerId,
            CreatedAt = order.Date,
            ShippingFee = order.ShippingFee ?? dto.ShippingFee,
            Status = "CREATED",
            Items = dto.Items
        };
    }

    public async Task<OrderStateChangeResponseDto> PickOrderAsync(int orderId)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync(IsolationLevel.Serializable);

        var state = await GetOrderStateForUpdate(orderId);

        if (state.Status == "PICKED")
        {
            await transaction.CommitAsync();
            return NoOp(orderId, state.Status);
        }

        if (state.Status != "CREATED")
        {
            throw new ConflictApiException("Cannot pick unless order is CREATED.");
        }

        var orderItems = await _context.CustomerOrderToItems
            .Where(i => i.OrderId == orderId)
            .ToListAsync();

        if (orderItems.Count == 0)
        {
            throw new ConflictApiException("Order has no items to pick.");
        }

        var groupedItems = orderItems
            .Where(i => i.ItemId.HasValue && i.Qty.HasValue)
            .GroupBy(i => i.ItemId!.Value)
            .Select(g => new { ItemId = g.Key, Quantity = g.Sum(x => x.Qty!.Value) })
            .ToList();

        foreach (var line in groupedItems)
        {
            var selectedBin = await _context.InventoryRecords
                .Where(i => i.ItemId == line.ItemId && i.Quantity >= line.Quantity)
                .OrderBy(i => i.Id)
                .FirstOrDefaultAsync();

            if (selectedBin == null)
            {
                throw new ConflictApiException($"Insufficient inventory for item {line.ItemId}.");
            }

            var rowsUpdated = await _context.Database.ExecuteSqlInterpolatedAsync(
                $"UPDATE warehouse.app_inventory SET quantity = quantity - {line.Quantity}, updated_at = now() WHERE id = {selectedBin.Id} AND quantity >= {line.Quantity}"
            );

            if (rowsUpdated == 0)
            {
                throw new ConflictApiException($"Insufficient inventory for item {line.ItemId} due to concurrent pick.");
            }
        }

        state.Status = "PICKED";
        state.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();
        await transaction.CommitAsync();

        return Changed(orderId, "PICKED");
    }

    public async Task<OrderStateChangeResponseDto> PackOrderAsync(int orderId)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync(IsolationLevel.Serializable);

        var state = await GetOrderStateForUpdate(orderId);

        if (state.Status == "PACKED")
        {
            await transaction.CommitAsync();
            return NoOp(orderId, state.Status);
        }

        if (state.Status != "PICKED")
        {
            throw new ConflictApiException("Cannot pack unless order is PICKED.");
        }

        state.Status = "PACKED";
        state.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();
        await transaction.CommitAsync();

        return Changed(orderId, "PACKED");
    }

    public async Task<OrderStateChangeResponseDto> ShipOrderAsync(int orderId)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync(IsolationLevel.Serializable);

        var state = await GetOrderStateForUpdate(orderId);

        if (state.Status == "SHIPPED")
        {
            await transaction.CommitAsync();
            return NoOp(orderId, state.Status);
        }

        if (state.Status != "PACKED")
        {
            throw new ConflictApiException("Cannot ship unless order is PACKED.");
        }

        state.Status = "SHIPPED";
        state.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();
        await transaction.CommitAsync();

        return Changed(orderId, "SHIPPED");
    }

    private async Task<OrderWorkflowState> GetOrderStateForUpdate(int orderId)
    {
        await _context.Database.ExecuteSqlInterpolatedAsync(
            $"SELECT id FROM warehouse.app_order_workflow_state WHERE order_id = {orderId} FOR UPDATE"
        );

        var state = await _context.OrderWorkflowStates
            .FirstOrDefaultAsync(s => s.OrderId == orderId);

        if (state == null)
        {
            var orderExists = await _context.CustomerOrders.AnyAsync(o => o.Id == orderId);
            if (!orderExists)
            {
                throw new NotFoundApiException("Order not found.");
            }

            throw new ConflictApiException("Order state is missing.");
        }

        return state;
    }

    private static OrderStateChangeResponseDto NoOp(int orderId, string status)
    {
        return new OrderStateChangeResponseDto
        {
            OrderId = orderId,
            Status = status,
            IdempotentNoOp = true
        };
    }

    private static OrderStateChangeResponseDto Changed(int orderId, string status)
    {
        return new OrderStateChangeResponseDto
        {
            OrderId = orderId,
            Status = status,
            IdempotentNoOp = false
        };
    }

    public async Task<OrderStatusDto> GetOrderStatusAsync(int orderId)
    {
        var order = await _context.CustomerOrders
            .Include(o => o.Customer)
            .Include(o => o.AppOrderWorkflowState)
            .Include(o => o.CustomerOrderToItems)
            .ThenInclude(oi => oi.Item)
            .FirstOrDefaultAsync(o => o.Id == orderId);

        if (order == null)
        {
            throw new NotFoundApiException($"Order with ID {orderId} not found.");
        }

        var items = order.CustomerOrderToItems
            .Select(oi => new OrderLineItemDto
            {
                Id = oi.Id,
                ItemId = oi.ItemId ?? 0,
                ItemName = oi.Item?.ItemName,
                Quantity = oi.Qty ?? 0,
                UnitPrice = oi.Price ?? 0
            })
            .ToList();

        var subTotal = items.Sum(i => i.LineTotal);

        // Try to find shipment information
        var shipment = await _context.Shipments
            .Where(s => s.OrderId == orderId)
            .FirstOrDefaultAsync();

        return new OrderStatusDto
        {
            OrderId = order.Id,
            CustomerId = order.CustomerId ?? 0,
            CustomerName = order.Customer?.Name,
            CreatedDate = order.Date,
            CurrentStatus = order.AppOrderWorkflowState?.Status ?? "UNKNOWN",
            StatusUpdatedAt = order.AppOrderWorkflowState?.UpdatedAt,
            Items = items,
            SubTotal = subTotal,
            ShippingFee = order.ShippingFee ?? 0,
            ShipmentId = shipment?.Id,
            ShipmentDate = shipment?.DateReceived
        };
    }
}

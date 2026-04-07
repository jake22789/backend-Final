using final_proj.Models;
using ClassLibrary.DTOs;

namespace final_proj.Services;

public interface IPurchaseOrderService
{
    Task<PurchaseOrder> CreatePurchaseOrderAsync(CreatePurchaseOrderDTO dto);
}

public class PurchaseOrderService : IPurchaseOrderService
{
    private readonly Db26TeamoneContext _context;
    private readonly ILogger<PurchaseOrderService> _logger;

    public PurchaseOrderService(Db26TeamoneContext context, ILogger<PurchaseOrderService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<PurchaseOrder> CreatePurchaseOrderAsync(CreatePurchaseOrderDTO dto)
    {
        try
        {
            var purchaseOrder = new PurchaseOrder
            {
                DateOrdered = dto.DateOrdered,
                OrderItems = new List<OrderItem>(),
                Shipments = new List<Shipment>()
            };

            _context.PurchaseOrders.Add(purchaseOrder);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"Purchase order created with ID: {purchaseOrder.Id}");

            return purchaseOrder;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error creating purchase order: {ex.Message}");
            throw;
        }
    }
}

//dto for purchase order and Orderitem
using System.ComponentModel.DataAnnotations;
namespace ClassLibrary.DTOs;

public class PurchaseOrderDTO
{
    public int Id { get; set; }

    [DataType(DataType.Date)]
    public DateTime? DateOrdered { get; set; }

    [Required(ErrorMessage = "Purchase order must contain at least one item.")]
    [MinLength(1, ErrorMessage = "At least one order item is required.")]
    public virtual ICollection<OrderItemDTO> OrderItems { get; set; } = new List<OrderItemDTO>();

    public virtual ICollection<ShipmentDTO> Shipments { get; set; } = new List<ShipmentDTO>();
}

public class OrderItemDTO
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Item ID is required.")]
    public int? ItemId { get; set; }

    public int? OrderId { get; set; }

    [Required(ErrorMessage = "Quantity is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than 0.")]
    public int? Quantity { get; set; }

    [Required(ErrorMessage = "Unit price is required.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Unit price must be greater than 0.")]
    public decimal? UnitPrice { get; set; }
}

public class ShipmentDTO
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Order ID is required for shipment.")]
    public int? OrderId { get; set; }

    [DataType(DataType.Date)]
    public DateTime? DateReceived { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "Price adjustment cannot be negative.")]
    public decimal? PriceAdjust { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "Handling cost cannot be negative.")]
    public decimal? HandlingCost { get; set; }
}
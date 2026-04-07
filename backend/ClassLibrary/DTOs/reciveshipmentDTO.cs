
using System.ComponentModel.DataAnnotations;
namespace ClassLibrary.DTOs;
public class ReceiveShipmentDTO
{
    [Required(ErrorMessage = "Shipment Id is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "Shipment Id must be a positive value.")]
    public int? Id { get; set; }

    [Required(ErrorMessage = "Order ID is required for shipment.")]
    public int? OrderId { get; set; }

    [DataType(DataType.Date)]
    public DateTime? DateReceived { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "Price adjustment cannot be negative.")]
    public decimal? PriceAdjust { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "Handling cost cannot be negative.")]
    public decimal? HandlingCost { get; set; }

    [Required(ErrorMessage = "At least one item shipment is required.")]
    [MinLength(1, ErrorMessage = "At least one item shipment is required.")]
    public virtual ICollection<itemShipmentDTO> ItemShipments { get; set; } = new List<itemShipmentDTO>();
}
public class itemShipmentDTO
{
    public int Id { get; set; }

    public int? ShipmentId { get; set; }

    [Required(ErrorMessage = "Item ID is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "Item ID must be a positive value.")]
    public int? ItemId { get; set; }

    [Required(ErrorMessage = "Quantity is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than 0.")]
    public int? Quantity { get; set; }

    public decimal? Discount { get; set; }
}
public class shipmentDTO
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

    public virtual ICollection<itemShipmentDTO> ItemShipments { get; set; } = new List<itemShipmentDTO>();
}

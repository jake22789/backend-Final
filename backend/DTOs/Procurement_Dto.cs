using System.ComponentModel.DataAnnotations;

namespace final_proj.DTO;

public class CreatePurchaseOrderRequestDto
{
    [Required]
    public int VendorId { get; set; }

    [Required]
    [MinLength(1, ErrorMessage = "A purchase order must contain one or more items")]
    public List<PurchaseOrderLineDto> Items { get; set; } = new();
}

public class PurchaseOrderLineDto
{
    [Required]
    public int ItemId { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than zero")]
    public int Quantity { get; set; }

    [Range(0.01, double.MaxValue, ErrorMessage = "Unit price must be greater than zero")]
    public decimal UnitPrice { get; set; }
}

public class PurchaseOrderResponseDto
{
    public int Id { get; set; }

    public int VendorId { get; set; }

    public DateTime? DateOrdered { get; set; }

    public List<PurchaseOrderLineDto> Items { get; set; } = new();
}

public class CreateShipmentRequestDto
{
    [Required]
    public int PurchaseOrderId { get; set; }

    [Range(0, double.MaxValue)]
    public decimal HandlingCost { get; set; }

    [Required]
    [MinLength(1)]
    public List<ShipmentLineDto> Items { get; set; } = new();
}

public class ShipmentLineDto
{
    [Required]
    public int ItemId { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than zero")]
    public int Quantity { get; set; }

    [Range(0.01, double.MaxValue, ErrorMessage = "Unit price must be greater than zero")]
    public decimal UnitPrice { get; set; }
}

public class ShipmentResponseDto
{
    public int Id { get; set; }

    public int PurchaseOrderId { get; set; }

    public DateTime? DateReceived { get; set; }

    public decimal HandlingCost { get; set; }

    public bool IsReceived { get; set; }

    public List<ShipmentLineDto> Items { get; set; } = new();
}

public class ReceiveShipmentResponseDto
{
    public int ShipmentId { get; set; }

    public bool AlreadyReceived { get; set; }

    public DateTime ReceivedAtUtc { get; set; }
}

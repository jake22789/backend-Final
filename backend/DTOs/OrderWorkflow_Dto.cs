using System.ComponentModel.DataAnnotations;

namespace final_proj.DTO;

public class CreateCustomerOrderRequestDto
{
    [Required]
    public int CustomerId { get; set; }

    [Range(0, double.MaxValue)]
    public decimal ShippingFee { get; set; }

    [Required]
    [MinLength(1, ErrorMessage = "Orders must contain one or more items")]
    public List<CustomerOrderLineRequestDto> Items { get; set; } = new();
}

public class CustomerOrderLineRequestDto
{
    [Required]
    public int ItemId { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than zero")]
    public int Quantity { get; set; }

    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero")]
    public decimal UnitPrice { get; set; }
}

public class OrderResponseDto
{
    public int Id { get; set; }

    public int CustomerId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public decimal ShippingFee { get; set; }

    public string Status { get; set; } = "CREATED";

    public List<CustomerOrderLineRequestDto> Items { get; set; } = new();
}

public class OrderStateChangeResponseDto
{
    public int OrderId { get; set; }

    public string Status { get; set; } = string.Empty;

    public bool IdempotentNoOp { get; set; }
}

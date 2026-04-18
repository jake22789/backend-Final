using System.ComponentModel.DataAnnotations;

namespace final_proj.DTO;

public class CreateOrderDto
{
    [Required]
    public int CustomerId { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "Shipping fee must be non-negative")]
    public decimal ShippingFee { get; set; }

    [Required]
    [MinLength(1, ErrorMessage = "At least one item is required")]
    public List<CreateOrderItemDto> Items { get; set; } = new();
}

public class CreateOrderItemDto
{
    [Required]
    public int ItemId { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1")]
    public int Qty { get; set; }

    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
    public decimal Price { get; set; }
}

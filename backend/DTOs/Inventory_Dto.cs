using System.ComponentModel.DataAnnotations;

namespace final_proj.DTO;

public class StoreInventoryRequestDto
{
    [Required]
    public int ItemId { get; set; }

    [Required]
    public int BinId { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than zero")]
    public int Quantity { get; set; }
}

public class InventoryResponseDto
{
    public int Id { get; set; }

    public int ItemId { get; set; }

    public int BinId { get; set; }

    public int Quantity { get; set; }
}

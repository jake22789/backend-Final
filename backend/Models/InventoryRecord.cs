namespace final_proj.Models;

public class InventoryRecord
{
    public int Id { get; set; }

    public int BinId { get; set; }

    public int ItemId { get; set; }

    public int Quantity { get; set; }

    public DateTime UpdatedAt { get; set; }

    public Item1? Item { get; set; }

    public Bin1? Bin { get; set; }
}

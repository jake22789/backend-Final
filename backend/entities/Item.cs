using System;
using System.Collections.Generic;

namespace final_proj.Models;

public partial class Item
{
    public int Id { get; set; }

    public string? ItemName { get; set; }

    public decimal? Cost { get; set; }

    public int? VendorId { get; set; }

    public decimal? ItemSize { get; set; }

    public virtual ICollection<BinLocation> BinLocations { get; set; } = new List<BinLocation>();

    public virtual ICollection<ItemShipment> ItemShipments { get; set; } = new List<ItemShipment>();

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual Vendor? Vendor { get; set; }
}

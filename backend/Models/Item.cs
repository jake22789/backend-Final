using System;
using System.Collections.Generic;

namespace final_proj.Models;

public partial class Item
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? Price { get; set; }

    public decimal? Volume { get; set; }

    public virtual ICollection<BinLocation> BinLocations { get; set; } = new List<BinLocation>();

    public virtual ICollection<CustomerOrderToItem1> CustomerOrderToItem1s { get; set; } = new List<CustomerOrderToItem1>();

    public virtual ICollection<ItemShipment1> ItemShipment1s { get; set; } = new List<ItemShipment1>();

    public virtual ICollection<PurchaseOrderItem1> PurchaseOrderItem1s { get; set; } = new List<PurchaseOrderItem1>();
}

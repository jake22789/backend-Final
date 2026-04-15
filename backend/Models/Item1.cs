using System;
using System.Collections.Generic;

namespace final_proj.Models;

public partial class Item1
{
    public int Id { get; set; }

    public string? ItemName { get; set; }

    public decimal? Price { get; set; }

    public decimal? ItemSize { get; set; }

    public virtual ICollection<BinLocation1> BinLocation1s { get; set; } = new List<BinLocation1>();

    public virtual ICollection<CustomerOrderToItem> CustomerOrderToItems { get; set; } = new List<CustomerOrderToItem>();

    public virtual ICollection<ItemShipment> ItemShipments { get; set; } = new List<ItemShipment>();

    public virtual ICollection<PurchaseOrderItem> PurchaseOrderItems { get; set; } = new List<PurchaseOrderItem>();
}

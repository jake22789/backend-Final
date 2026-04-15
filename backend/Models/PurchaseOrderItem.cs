using System;
using System.Collections.Generic;

namespace final_proj.Models;

public partial class PurchaseOrderItem
{
    public int Id { get; set; }

    public int? Quantity { get; set; }

    public decimal? Price { get; set; }

    public int? ItemId { get; set; }

    public int? OrderId { get; set; }

    public virtual Item1? Item { get; set; }

    public virtual PurchaseOrder1? Order { get; set; }
}

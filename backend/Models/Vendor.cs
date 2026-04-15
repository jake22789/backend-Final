using System;
using System.Collections.Generic;

namespace final_proj.Models;

public partial class Vendor
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; } = new List<PurchaseOrder>();
}

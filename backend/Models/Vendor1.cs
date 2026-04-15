using System;
using System.Collections.Generic;

namespace final_proj.Models;

public partial class Vendor1
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<PurchaseOrder1> PurchaseOrder1s { get; set; } = new List<PurchaseOrder1>();
}

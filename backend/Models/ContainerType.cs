using System;
using System.Collections.Generic;

namespace final_proj.Models;

public partial class ContainerType
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public decimal? Volume { get; set; }

    public virtual ICollection<ShippingContainer1> ShippingContainer1s { get; set; } = new List<ShippingContainer1>();
}

using System;
using System.Collections.Generic;

namespace final_proj.Models;

public partial class ContainerType1
{
    public int Id { get; set; }

    public decimal? Volume { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<ShippingContainer> ShippingContainers { get; set; } = new List<ShippingContainer>();
}

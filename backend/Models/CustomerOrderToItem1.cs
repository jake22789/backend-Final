using System;
using System.Collections.Generic;

namespace final_proj.Models;

public partial class CustomerOrderToItem1
{
    public int Id { get; set; }

    public int? OrderId { get; set; }

    public int? ItemId { get; set; }

    public int? Qty { get; set; }

    public int? Price { get; set; }

    public virtual ICollection<CustomerOrderToContainer> CustomerOrderToContainers { get; set; } = new List<CustomerOrderToContainer>();

    public virtual Item? Item { get; set; }

    public virtual CustomerOrder1? Order { get; set; }
}

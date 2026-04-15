using System;
using System.Collections.Generic;

namespace final_proj.Models;

public partial class CustomerOrderToItem
{
    public int Id { get; set; }

    public int? OrderId { get; set; }

    public int? ItemId { get; set; }

    public int? Qty { get; set; }

    public decimal? Price { get; set; }

    public virtual ICollection<CustomerOrderToContainer1> CustomerOrderToContainer1s { get; set; } = new List<CustomerOrderToContainer1>();

    public virtual Item1? Item { get; set; }

    public virtual CustomerOrder? Order { get; set; }
}

using System;
using System.Collections.Generic;

namespace final_proj.Models;

public partial class CustomerOrder
{
    public int Id { get; set; }

    public decimal? ShippingFee { get; set; }

    public int? CustomerId { get; set; }

    public DateTime? Date { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual ICollection<CustomerOrderToItem> CustomerOrderToItems { get; set; } = new List<CustomerOrderToItem>();
}

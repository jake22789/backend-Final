using System;
using System.Collections.Generic;

namespace final_proj.Models;

public partial class CustomerOrder1
{
    public int Id { get; set; }

    public int? CustomerId { get; set; }

    public decimal? ShippingFee { get; set; }

    public DateTime? Date { get; set; }

    public virtual Customer1? Customer { get; set; }

    public virtual ICollection<CustomerOrderToItem1> CustomerOrderToItem1s { get; set; } = new List<CustomerOrderToItem1>();
}

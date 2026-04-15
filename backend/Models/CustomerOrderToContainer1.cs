using System;
using System.Collections.Generic;

namespace final_proj.Models;

public partial class CustomerOrderToContainer1
{
    public int Id { get; set; }

    public int? Qty { get; set; }

    public int? OrderItemId { get; set; }

    public int? ShippingContainer { get; set; }

    public virtual CustomerOrderToItem? OrderItem { get; set; }

    public virtual ShippingContainer1? ShippingContainerNavigation { get; set; }
}

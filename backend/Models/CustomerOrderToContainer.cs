using System;
using System.Collections.Generic;

namespace final_proj.Models;

public partial class CustomerOrderToContainer
{
    public int Id { get; set; }

    public int? OrderItemId { get; set; }

    public int? ShippingContainer { get; set; }

    public int? Qty { get; set; }

    public virtual CustomerOrderToItem1? OrderItem { get; set; }

    public virtual ShippingContainer? ShippingContainerNavigation { get; set; }
}

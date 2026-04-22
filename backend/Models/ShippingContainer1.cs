using System;
using System.Collections.Generic;

namespace final_proj.Models;

public partial class ShippingContainer1
{
    public int Id { get; set; }

    public int? ContainerType { get; set; }

    public int? CarrierId { get; set; }

    public decimal? ShippingFee { get; set; }

    public DateTime? Date { get; set; }

    public string? TrackingNumber { get; set; }

    public string? ShippingAddress { get; set; }

    public virtual Carrier? Carrier { get; set; }

    public virtual ContainerType? ContainerTypeNavigation { get; set; }

    public virtual ICollection<CustomerOrderToContainer1> CustomerOrderToContainer1s { get; set; } = new List<CustomerOrderToContainer1>();
}

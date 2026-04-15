using System;
using System.Collections.Generic;

namespace final_proj.Models;

public partial class ShippingContainer
{
    public int Id { get; set; }

    public decimal? ShippingFee { get; set; }

    public DateTime? Date { get; set; }

    public int? CarrierId { get; set; }

    public string? TrackingNumber { get; set; }

    public int? TypeId { get; set; }

    public virtual Carrier1? Carrier { get; set; }

    public virtual ICollection<CustomerOrderToContainer> CustomerOrderToContainers { get; set; } = new List<CustomerOrderToContainer>();

    public virtual ContainerType1? Type { get; set; }
}

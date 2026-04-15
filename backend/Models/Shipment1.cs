using System;
using System.Collections.Generic;

namespace final_proj.Models;

public partial class Shipment1
{
    public int Id { get; set; }

    public int? OrderId { get; set; }

    public DateTime? DateReceived { get; set; }

    public decimal? HandlingCost { get; set; }

    public virtual ICollection<ItemShipment1> ItemShipment1s { get; set; } = new List<ItemShipment1>();

    public virtual PurchaseOrder? Order { get; set; }
}

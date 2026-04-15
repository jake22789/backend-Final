using System;
using System.Collections.Generic;

namespace final_proj.Models;

public partial class ItemShipment1
{
    public int Id { get; set; }

    public int? ItemId { get; set; }

    public int? ShipmentId { get; set; }

    public int? ActionId { get; set; }

    public int? Quantity { get; set; }

    public decimal? UnitPrice { get; set; }

    public virtual ShipmentAction1? Action { get; set; }

    public virtual Item? Item { get; set; }

    public virtual Shipment1? Shipment { get; set; }
}

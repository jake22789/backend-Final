using System;
using System.Collections.Generic;

namespace final_proj.Models;

public partial class ShipmentAction1
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<ItemShipment1> ItemShipment1s { get; set; } = new List<ItemShipment1>();
}

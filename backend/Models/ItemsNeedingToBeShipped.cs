using System;
using System.Collections.Generic;

namespace final_proj.Models;

public partial class ItemsNeedingToBeShipped
{
    public int? OrderId { get; set; }

    public DateTime? OrderDate { get; set; }

    public int? ItemId { get; set; }

    public long? QtyOrdered { get; set; }

    public long? QtyAlreadyShipped { get; set; }

    public long? QtyRemainingToBeShipped { get; set; }

    public long? QuantityInWarehouse { get; set; }
}

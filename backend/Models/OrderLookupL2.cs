using System;
using System.Collections.Generic;

namespace final_proj.Models;

public partial class OrderLookupL2
{
    public int? OrderId { get; set; }

    public DateTime? OrderDate { get; set; }

    public int? ItemId { get; set; }

    public decimal? QtyOrdered { get; set; }

    public decimal? QtyAlreadyShipped { get; set; }

    public decimal? QtyRemainingToBeShipped { get; set; }

    public long? QtyInWarehouse { get; set; }
}

using System;
using System.Collections.Generic;

namespace final_proj.Models;

public partial class OrderLookupL31
{
    public int? OrderId { get; set; }

    public DateTime? OrderDate { get; set; }

    public int? ItemId { get; set; }

    public decimal? QtyOrdered { get; set; }

    public int? BoxId { get; set; }

    public string? BoxTrackingId { get; set; }

    public long? QtyInBox { get; set; }

    public DateTime? DateShipped { get; set; }
}

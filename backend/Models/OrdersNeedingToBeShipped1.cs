using System;
using System.Collections.Generic;

namespace final_proj.Models;

public partial class OrdersNeedingToBeShipped1
{
    public int? OrderId { get; set; }

    public DateTime? OrderDate { get; set; }

    public int? ItemId { get; set; }

    public string? QtyOrdered { get; set; }

    public string? QtyAlreadyShipped { get; set; }

    public string? QtyRemainingToBeShipped { get; set; }
}

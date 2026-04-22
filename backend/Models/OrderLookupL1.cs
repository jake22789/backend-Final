using System;
using System.Collections.Generic;

namespace final_proj.Models;

public partial class OrderLookupL1
{
    public int? Id { get; set; }

    public DateTime? Date { get; set; }

    public string? Address { get; set; }

    public string? TFEverythingShipped { get; set; }
}

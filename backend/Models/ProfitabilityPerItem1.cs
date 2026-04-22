using System;
using System.Collections.Generic;

namespace final_proj.Models;

public partial class ProfitabilityPerItem1
{
    public int? ItemId { get; set; }

    public string? Name { get; set; }

    public long? QtySold { get; set; }

    public long? AvgCostToPurchase { get; set; }

    public long? AvgRevenuePerSale { get; set; }

    public long? ProfitPerUnit { get; set; }
}

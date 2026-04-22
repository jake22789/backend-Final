using System;
using System.Collections.Generic;

namespace final_proj.Models;

public partial class ProfitabilityPerItem
{
    public int? ItemId { get; set; }

    public string? Name { get; set; }

    public long? QtySold { get; set; }

    public decimal? AvgCostToPurchase { get; set; }

    public decimal? AvgRevenuePerSale { get; set; }

    public decimal? ProfitPerUnit { get; set; }
}

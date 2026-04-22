namespace final_proj.DTO;


public class InventoryItemDto
{
    public int ItemId { get; set; }

    public string? ItemName { get; set; }

    public int? Price { get; set; }

    public decimal? Volume { get; set; }

    public int TotalQuantityAvailable { get; set; }
}


public class InventoryLocationDto
{
    public int InventoryRecordId { get; set; }

    public int ItemId { get; set; }

    public string? ItemName { get; set; }

    public int BinId { get; set; }

    public int Quantity { get; set; }

    public DateTime UpdatedAt { get; set; }
}


public class InventoryReportResponseDto
{
    public List<InventoryItemDto> Items { get; set; } = new();

    public int TotalUniqueItems { get; set; }

    public int TotalQuantity { get; set; }
}

public class InventoryDetailedReportResponseDto
{
    public List<InventoryLocationDto> Locations { get; set; } = new();

    public int TotalLocations { get; set; }

    public int TotalQuantity { get; set; }
}

public class InventoryOnHandDto
{
    public int? Id { get; set; }

    public string? ItemName { get; set; }

    public long? QtyInWarehouse { get; set; }

    public long? QtySoldNotYetShipped { get; set; }

    public long? QtyAvailableForSale { get; set; }
}

public class ItemsNeedingToBeShippedDto
{
    public int? OrderId { get; set; }

    public DateTime? OrderDate { get; set; }

    public int? ItemId { get; set; }

    public long? QtyOrdered { get; set; }

    public long? QtyAlreadyShipped { get; set; }

    public long? QtyRemainingToBeShipped { get; set; }

    public long? QuantityInWarehouse { get; set; }
}

public class OrdersNeedingToBeShippedDto
{
    public int? OrderId { get; set; }

    public DateTime? OrderDate { get; set; }

    public int? ItemId { get; set; }

    public string? QtyOrdered { get; set; }

    public string? QtyAlreadyShipped { get; set; }

    public string? QtyRemainingToBeShipped { get; set; }
}

public class ProfitabilityPerItemDto
{
    public int? ItemId { get; set; }

    public string? Name { get; set; }

    public long? QtySold { get; set; }

    public decimal? AvgCostToPurchase { get; set; }

    public decimal? AvgRevenuePerSale { get; set; }

    public decimal? ProfitPerUnit { get; set; }
}

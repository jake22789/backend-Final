namespace final_proj.DTO;


public class OrderLineItemDto
{
    public int Id { get; set; }

    public int ItemId { get; set; }

    public string? ItemName { get; set; }

    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public decimal LineTotal => Quantity * UnitPrice;
}


public class OrderStatusDto
{
    public int OrderId { get; set; }

    public int CustomerId { get; set; }

    public string? CustomerName { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string CurrentStatus { get; set; } = string.Empty;

    public DateTime? StatusUpdatedAt { get; set; }

    public List<OrderLineItemDto> Items { get; set; } = new();

    public decimal SubTotal { get; set; }

    public decimal ShippingFee { get; set; }

    public decimal GrandTotal => SubTotal + ShippingFee;

    public int? ShipmentId { get; set; }

    public DateTime? ShipmentDate { get; set; }

    public string? TrackingInfo { get; set; }
}

public class OrderStatusResponseDto
{
    public OrderStatusDto? Order { get; set; }

    public string Message { get; set; } = string.Empty;

    public bool IsSuccess { get; set; }
}

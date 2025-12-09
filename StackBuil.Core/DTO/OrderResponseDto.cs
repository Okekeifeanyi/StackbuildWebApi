using StackBuildApi.Core.DTO;

public class OrderResponseDto
{
    public Guid OrderId { get; set; }
    public DateTime CreatedAt { get; set; }
    public decimal Total { get; set; }
    public List<OrderItemDto> Items { get; set; } = new();

    public OrderResponseDto(Guid orderId, DateTime createdAt, decimal total, List<OrderItemDto> items)
    {
        OrderId = orderId;
        CreatedAt = createdAt;
        Total = total;
        Items = items;
    }
}

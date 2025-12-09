public class OrderItemDto
{
    public Guid ProductId { get; set; }
    public string Name { get; set; } = null!;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal LineTotal { get; set; }

    public OrderItemDto(Guid productId, string name, int quantity, decimal unitPrice, decimal lineTotal)
    {
        ProductId = productId;
        Name = name;
        Quantity = quantity;
        UnitPrice = unitPrice;
        LineTotal = lineTotal;
    }
}

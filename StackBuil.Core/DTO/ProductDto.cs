public class ProductDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }

    public ProductDto(Guid id, string name, string? description, decimal price, int stockQuantity)
    {
        Id = id;
        Name = name;
        Description = description;
        Price = price;
        StockQuantity = stockQuantity;
    }
}

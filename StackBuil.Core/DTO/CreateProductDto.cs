// File: StackBuildApi.Core/DTO/CreateProductDto.cs

namespace StackBuildApi.Core.DTO
{
    public class CreateProductDto
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
    }
}
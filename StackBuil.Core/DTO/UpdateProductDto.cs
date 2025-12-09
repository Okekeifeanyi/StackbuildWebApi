// File: StackBuildApi.Core/DTO/UpdateProductDto.cs

using System;

namespace StackBuildApi.Core.DTO
{
    public class UpdateProductDto : CreateProductDto
    {
        public Guid Id { get; set; }

        // Optional: parameterless constructor (useful for model binding in controllers)
        public UpdateProductDto() { }

        // Optional but nice to have – explicit constructor
        public UpdateProductDto(Guid id, string name, string? description, decimal price, int stockQuantity)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            StockQuantity = stockQuantity;
        }
    }
}
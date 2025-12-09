using StackBuildApi.Model.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace StackBuildApi.Model.Entities
{
    public class Product : BaseEntity
    {
      
         
            public string Name { get; set; } = null!;
            public string? Description { get; set; }
            public decimal Price { get; set; }
            public int StockQuantity { get; set; }
        

    }
}

using StackBuildApi.Model.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace StackBuildApi.Model.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }

        [Column(TypeName = "numeric(18,2)")]
        public decimal Price { get; set; }

        public int StockQuantity { get; set; }
    }
}

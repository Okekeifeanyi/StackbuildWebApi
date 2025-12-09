using StackBuildApi.Model.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace StackBuildApi.Model.Entities
{
    public class OrderItem : BaseEntity
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; } = null!;

        public int Quantity { get; set; }

        [Column(TypeName = "numeric(18,2)")]
        public decimal UnitPrice { get; set; }

        public string OrderId { get; set; }
        public Order Order { get; set; } = null!;
    }
}

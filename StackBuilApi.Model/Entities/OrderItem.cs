using StackBuildApi.Model.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StackBuildApi.Domain.Entities
{
    public class OrderItem : BaseEntity
    {
       

        [ForeignKey(nameof(Order))]
        public Guid OrderId { get; set; }
        public Order? Order { get; set; }

        [ForeignKey(nameof(Product))]
        public Guid ProductId { get; set; }
        public Product? Product { get; set; }

        public int Quantity { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; }
        public decimal SubTotal => UnitPrice * Quantity;

       
    }
}

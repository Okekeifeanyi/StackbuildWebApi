using StackBuildApi.Domain.Entities;
using StackBuildApi.Model.Entities;

namespace StackBuildApi.Model.Entities
{
    public class Order : BaseEntity
    {
   
        public decimal Total { get; set; }

        public List<OrderItem> Items { get; set; } = new();
    }
}

using StackBuildApi.Model.Entities;

namespace StackBuildApi.Model.Entities
{
    public class Order : BaseEntity
    {
        public decimal TotalAmount { get; set; }

        public List<OrderItem> Items { get; set; } = new();
    }
}

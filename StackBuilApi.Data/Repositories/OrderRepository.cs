using StackBuilApi.Core.Interface.irepositories;
using StackBuildApi.Data.Database;
using StackBuildApi.Model.Entities;

namespace StackBuildApi.Data.Repositories
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(StackBuildDB context) : base(context)
        {
        }
    }
}

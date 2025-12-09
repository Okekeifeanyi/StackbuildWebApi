using StackBuildApi.Core.Interface.irepositories;
using StackBuildApi.Data.Database;
using StackBuildApi.Data.Repositories;
using StackBuildApi.Model.Entities;

namespace StackBuilApi.Data.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(StackBuildDB context) : base(context)
        {
        }
    }
}

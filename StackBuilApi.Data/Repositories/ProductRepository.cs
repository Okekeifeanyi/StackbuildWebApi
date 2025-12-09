using Microsoft.EntityFrameworkCore;
using StackBuildApi.Data.Database;
using StackBuildApi.Data.Repositories;
using StackBuildApi.Model.Entities;

namespace StackBuilApi.Data.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly StackBuildDB _context;
        public ProductRepository(StackBuildDB context) : base(context)
        {
            _context = context;
        }
        public async Task<List<Product>> GetByIdsAsync(List<Guid> ids)
        {
            return await _context.Products
                .Where(x => ids.Contains(x.Id))
                .ToListAsync();
        }
        public async Task<List<Product>> GetByIdsForUpdateAsync(List<Guid> ids)
        {
            return await _context.Products
                .Where(x => ids.Contains(x.Id))
                .TagWith("FOR UPDATE") 
                .ToListAsync();
        }

    }
}

using Microsoft.EntityFrameworkCore;
using StackBuilApi.Core.Interface.irepositories;
using StackBuildApi.Model.Entities;

public interface IProductRepository : IGenericRepository<Product>
{
    Task<List<Product>> GetByIdsAsync(List<Guid> ids);
    
}

using Microsoft.EntityFrameworkCore.Storage;
using StackBuilApi.Core.Interface.irepositories;

namespace StackBuildApi.Core.Interface.irepositories
{
    public interface IUnitOfWork
    {
        IProductRepository ProductRepository { get; }
        IOrderRepository OrderRepository { get; }
        Task<int> SaveChangesAsync();
        Task<IDbContextTransaction> BeginTransactionAsync();
    }
}

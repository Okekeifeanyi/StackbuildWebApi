using Microsoft.EntityFrameworkCore.Storage;
using StackBuilApi.Core.Interface.irepositories;
using StackBuilApi.Data.Repositories;
using StackBuildApi.Core.Interface.irepositories;
using StackBuildApi.Data.Database;
using StackBuildApi.Data.Repositories;

namespace StackBuildApi.Data.UOW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StackBuildDB _context;
        private bool _disposed;

        public UnitOfWork(StackBuildDB context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));

            ProductRepository = new ProductRepository(_context);
            OrderRepository = new OrderRepository(_context);
        }

        public IProductRepository ProductRepository { get; }
        public IOrderRepository OrderRepository { get; }

        public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();

        public Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return _context.Database.BeginTransactionAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                _context.Dispose();
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

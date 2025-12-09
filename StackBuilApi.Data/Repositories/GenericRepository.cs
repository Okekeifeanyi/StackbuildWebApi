using Microsoft.EntityFrameworkCore;
using StackBuilApi.Core.Interface.irepositories;
using StackBuildApi.Data.Database;
using System.Linq.Expressions;

namespace StackBuildApi.Data.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly StackBuildDB _stackBuildDB;

        public GenericRepository(StackBuildDB context)
        {
            _stackBuildDB = context;
        }

        public async Task AddAsync(T entity)
        {
            await _stackBuildDB.Set<T>().AddAsync(entity);
        }

        public void Delete(T entity)
        {
            _stackBuildDB.Set<T>().Remove(entity);
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            _stackBuildDB.Set<T>().RemoveRange(entities);
        }

        public async Task<T?> FindSingleAsync(Expression<Func<T, bool>> expression)
        {
            return await _stackBuildDB.Set<T>().FirstOrDefaultAsync(expression);
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _stackBuildDB.Set<T>().ToListAsync();
        }

        public async Task<T?> GetByIdAsync(Guid id)
        {
            return await _stackBuildDB.Set<T>().FindAsync(id);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _stackBuildDB.SaveChangesAsync();
        }

        public IQueryable<T> AsQueryable()
        {
            return _stackBuildDB.Set<T>().AsQueryable();
        }

        public void Update(T entity)
        {
            _stackBuildDB.Set<T>().Update(entity);
        }
    }
}

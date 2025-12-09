using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace StackBuilApi.Core.Interface.irepositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task AddAsync(T entity);
        void Delete(T entity);
        void DeleteRange(IEnumerable<T> entities);
        Task<T?> FindSingleAsync(Expression<Func<T, bool>> expression);
        Task<List<T>> GetAllAsync();
        Task<T?> GetByIdAsync(Guid id);
        Task<int> SaveChangesAsync();
        IQueryable<T> AsQueryable();
        void Update(T entity);
    }
}

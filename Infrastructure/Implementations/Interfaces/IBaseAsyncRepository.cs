using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infrastructure.Implementations
{
    public interface IBaseAsyncRepository<T> where T : class
    {
        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> query, Expression<Func<T, object>> orderBy = null, bool isDesc = false, int maximumRows = 0, int startRowIndex = 0);
        Task<IReadOnlyList<T>> ListAllAsync();
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<int> CountAsync(Expression<Func<T, bool>> query);
    }
}
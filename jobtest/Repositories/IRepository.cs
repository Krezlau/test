using System.Linq.Expressions;
using jobtest.Models;

namespace jobtest.Repositories;

public interface IRepository<T> where T : class
{
    Task<T?> GetAsync(Expression<Func<T, bool>>? filter = null);

    Task<PageResultDTO<T>> GetPageAsync(int pageNumber,
        int pageSize,
        Expression<Func<T, bool>>? filter = null);
    
    Task CreateAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
}
using System.Linq.Expressions;
using jobtest.DbContext;
using jobtest.Models;
using Microsoft.EntityFrameworkCore;

namespace jobtest.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly AppDbContext _context;
    private readonly DbSet<T> _dbSet;

    public Repository(AppDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public async Task<T?> GetAsync(Expression<Func<T, bool>>? filter = null)
    {
        IQueryable<T> query = _dbSet;
        
        if (filter is not null)
        {
            query = query.Where(filter);
        }
        
        return await query.FirstOrDefaultAsync();
    }

    public async Task<PageResultDTO<T>> GetPageAsync(int pageNumber,
                                                    int pageSize,
                                                    Expression<Func<T, bool>>? filter = null)
    {
        IQueryable<T> query = _dbSet;
        
        if (filter is not null)
        {
            query = query.Where(filter);
        }
        
        var count = await query.CountAsync();
        var pageCount = (int) Math.Ceiling((double) count / pageSize);
        var result = await query.Skip(pageNumber * pageSize)
            .Take(pageSize)
            .ToListAsync();
        
        return new PageResultDTO<T>
        {
            PageNumber = pageNumber,
            PageCount = pageCount,
            PageSize = pageSize,
            QueryResult = result
        };
    }

    public async Task CreateAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(T entity)
    {
        _dbSet.Remove(entity);
        await _context.SaveChangesAsync();
    }
}
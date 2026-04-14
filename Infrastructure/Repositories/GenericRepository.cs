using Domain.Interfaces.Repositories;
using Domain.Pagination;
using Infrastructure.Context;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class GenericRepository<T>(DataContext context) : IGenericRepository<T> where T : class
{
    protected readonly DataContext _context = context;
    protected readonly DbSet<T> _dbSet = context.Set<T>();

    public async Task<T?> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<PagedList<T>> GetAllPagedAsync(PaginationParams paginationParams)
    {
        return await _dbSet.AsNoTracking().ToPagedListAsync(paginationParams.PageNumber, paginationParams.PageSize);
    }

    public async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public void Update(T entity)
    {
        _dbSet.Update(entity);
    }

    public void Delete(T entity)
    {
        _dbSet.Remove(entity);
    }
}

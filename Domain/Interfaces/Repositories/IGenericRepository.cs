using Domain.Pagination;

namespace Domain.Interfaces.Repositories;

public interface IGenericRepository<T> where T : class
{
    Task<T?> GetByIdAsync(int id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<PagedList<T>> GetAllPagedAsync(PaginationParams paginationParams);
    Task AddAsync(T entity);
    void Update(T entity);
    void Delete(T entity);
}

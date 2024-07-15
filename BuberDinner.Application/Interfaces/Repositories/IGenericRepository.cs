using BuberDinner.Domain.Wrapper;

namespace BuberDinner.Application.Interfaces.Repositories;

public interface IGenericRepository<T, TKey> where T : class
{
    Task<T?> GetByIdAsync(TKey id);
    Task<PagedList<T>> GetAllAsync();
    Task<T> AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
}

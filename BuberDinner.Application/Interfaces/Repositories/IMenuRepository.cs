using BuberDinner.Domain.Menus;
using BuberDinner.Domain.Menus.ValueObjects;
using BuberDinner.Domain.Wrapper;

namespace BuberDinner.Application.Interfaces.Repositories;

public interface IMenuRepository 
{
    Task<Menu> AddAsync(Menu entity);
    Task DeleteAsync(Menu entity);
    Task<PagedList<Menu>> GetAllAsync(int pageNumber, int pageSize);
    Task<Menu?> GetByIdAsync(MenuId id);
    Task UpdateAsync(Menu entity);
}

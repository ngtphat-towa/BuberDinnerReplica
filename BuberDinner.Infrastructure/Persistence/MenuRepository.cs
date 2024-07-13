using BuberDinner.Application.Interfaces.Repositories;
using BuberDinner.Domain.Menus;
using BuberDinner.Domain.Menus.ValueObjects;
using BuberDinner.Domain.Wrapper;

using Microsoft.EntityFrameworkCore;

namespace BuberDinner.Infrastructure.Persistence;

public class MenuRepository : IMenuRepository
{

    private readonly List<Menu> _context;

    public MenuRepository()
    {
        _context = new();
    }

    public async Task<Menu> AddAsync(Menu menu)
    {
        await Task.CompletedTask;

        _context.Add(menu);


        return menu;
    }

    public async Task DeleteAsync(Menu entity)
    {
        await Task.CompletedTask;

        _context.Remove(entity);

    }

    public async Task<PagedList<Menu>> GetAllAsync(int pageNumber, int pageSize)
    {
        await Task.CompletedTask;
        var source = _context.AsQueryable();
        var count = await source.CountAsync();
        var items = await source.Skip((pageNumber - 1) * pageSize)
                                 .Take(pageSize)
                                 .ToListAsync();

        return new PagedList<Menu>(items, count, pageNumber, pageSize);
    }


    public async Task<Menu?> GetByIdAsync(MenuId id)
    {
        await Task.CompletedTask;

        return _context
          .FirstOrDefault(m => m.Id.Value == id.Value);
    }

    public async Task UpdateAsync(Menu entity)
    {
        await Task.CompletedTask;

        _context.Add(entity);

    }
}

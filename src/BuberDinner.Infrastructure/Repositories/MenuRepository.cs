using BuberDinner.Application.Interfaces.Repositories;
using BuberDinner.Domain.Menus;
using BuberDinner.Domain.Menus.ValueObjects;
using BuberDinner.Domain.Wrapper;
using BuberDinner.Infrastructure.Context;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BuberDinner.Infrastructure.Repositories
{
    public class MenuRepository : IMenuRepository, IDisposable
    {
        private readonly BuberDinnerDbContext _context;
        private readonly ILogger<MenuRepository> _logger;

        public MenuRepository(BuberDinnerDbContext context, ILogger<MenuRepository> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Menu> AddAsync(Menu menu)
        {
            try
            {
                await _context.Menus.AddAsync(menu);
                await _context.SaveChangesAsync();
                return menu;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred in AddAsync method of MenuRepository.");
                throw;
            }
        }

        public async Task DeleteAsync(Menu entity)
        {
            try
            {
                _context.Menus.Remove(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred in DeleteAsync method of MenuRepository.");
                throw;
            }
        }

        public async Task<PagedList<Menu>> GetAllAsync(int pageNumber, int pageSize)
        {
            try
            {
                var count = await _context.Menus
                    .AsNoTracking()
                    .AsSingleQuery()
                    .CountAsync();
                var items = await _context.Menus
                    .AsNoTracking()
                    .AsSingleQuery()
                    .OrderBy(m => m.Id)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                return new PagedList<Menu>(items, count, pageNumber, pageSize);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred in GetAllAsync method of MenuRepository.");
                throw;
            }
        }

        public async Task<Menu?> GetByIdAsync(MenuId id)
        {
            try
            {
                return await _context.Menus
                    .FirstOrDefaultAsync(m => m.Id.Value == id.Value);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred in GetByIdAsync method of MenuRepository.");
                throw;
            }
        }

        public async Task UpdateAsync(Menu entity)
        {
            try
            {
                _context.Entry(entity).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred in UpdateAsync method of MenuRepository.");
                throw;
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

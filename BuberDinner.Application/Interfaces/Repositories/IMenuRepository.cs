using BuberDinner.Domain.Menus;

namespace BuberDinner.Application.Interfaces.Repositories;

public interface IMenuRepository
{
    Task Add(Menu menu);
}

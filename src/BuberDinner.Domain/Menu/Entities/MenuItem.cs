using BuberDinner.Domain.Common.Models;
using BuberDinner.Domain.Menus.ValueObjects;

namespace BuberDinner.Domain.Menus.Entities;

public sealed class MenuItem : Entity<MenuItemId>
{
    public string Name { get; } = string.Empty;
    public string Description { get; } = string.Empty;


    private MenuItem(MenuItemId menuItemId, string name, string description)
        : base(menuItemId)
    {
        Name = name;
        Description = description;
    }

    public static MenuItem Create(string name, string description)
    {
        return new(MenuItemId.CreateUnique(), name, description);
    }

    private MenuItem() :base(MenuItemId.CreateUnique()) { }
}
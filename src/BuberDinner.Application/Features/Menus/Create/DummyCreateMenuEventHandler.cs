using BuberDinner.Domain.Menus.Events;

using MediatR;

namespace BuberDinner.Application.Features.Menus.Create;

public class DummyCreateMenuEventHandler : INotificationHandler<MenuCreated>
{
    public async Task Handle(MenuCreated notification, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        Console.WriteLine($"Menu created: {notification.Menu.Name}");
    }
}

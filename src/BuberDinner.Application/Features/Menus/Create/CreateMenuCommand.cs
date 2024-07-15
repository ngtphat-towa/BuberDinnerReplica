using ErrorOr;
using MediatR;
using BuberDinner.Domain.Menus;
using BuberDinner.Application.Interfaces.Repositories;
using BuberDinner.Domain.Common.ValueObjects;
using BuberDinner.Domain.Host.ValueObjects;
using BuberDinner.Domain.Menus.Entities;

namespace BuberDinner.Application.Features.Menus.Create;

public record CreateMenuCommand(
    string HostId,
    string Name,
    string Description,
    List<MenuSectionCommand> Sections) : IRequest<ErrorOr<Menu>>;

public record MenuSectionCommand(
    string Name,
    string Description,
    List<MenuItemCommand> Items);

public record MenuItemCommand(
    string Name,
    string Description);

public class CreateMenuCommandHandler : IRequestHandler<CreateMenuCommand, ErrorOr<Menu>>
{
    private readonly IMenuRepository _menuRepository;

    public CreateMenuCommandHandler(IMenuRepository menuRepository)
    {
        _menuRepository = menuRepository;
    }

    public async Task<ErrorOr<Menu>> Handle(CreateMenuCommand request, CancellationToken cancellationToken)
    {
        var menu = Menu.Create(
            hostId: HostId.GetId(request.HostId),
            name: request.Name,
            description: request.Description,
            averageRating: AverageRating.CreateNew(),
            sections: request.Sections.ConvertAll(sections => MenuSection.Create(
                name: sections.Name,
                description: sections.Description,
                items: sections.Items.ConvertAll(item => MenuItem.Create(
                    name: item.Name,
                    description: item.Description)))));

        await _menuRepository.AddAsync(menu);

        return menu;
    }
}

using BuberDinner.Application.Interfaces.Repositories;
using BuberDinner.Domain.Menus.ValueObjects;
using ErrorOr;
using MediatR;

namespace BuberDinner.Application.Menus.Update;

public class UpdateMenuItemCommandHandler : IRequestHandler<UpdateMenuItemCommand, ErrorOr<Unit>>
{
    private readonly IMenuRepository _menuRepository;

    public UpdateMenuItemCommandHandler(IMenuRepository menuRepository)
    {
        _menuRepository = menuRepository;
    }

    public async Task<ErrorOr<Unit>> Handle(UpdateMenuItemCommand request, CancellationToken cancellationToken)
    {
        var menu = await _menuRepository.GetByIdAsync(MenuId.GetId(request.MenuId));
        if (menu is null) return Error.NotFound($"Menu with ID {request.MenuId} not found.");

        var section = menu.Sections.FirstOrDefault(sec => sec.Id == MenuSectionId.GetId(request.SectionId));
        if (section is null) return Error.NotFound($"Section with ID {request.SectionId} not found.");

        var item = section.Items.FirstOrDefault(i => i.Id == MenuItemId.GetId(request.ItemId));
        if (item is null) return Error.NotFound($"Item with ID {request.ItemId} not found.");

        // Update item details
        //item.Update(request.Name, request.Description);

        await _menuRepository.UpdateAsync(menu);

        return Unit.Value;
    }
}
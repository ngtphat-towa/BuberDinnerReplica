using BuberDinner.Application.Interfaces.Repositories;
using BuberDinner.Domain.Menus.ValueObjects;

using ErrorOr;
using FluentValidation;

using MediatR;

namespace BuberDinner.Application.Menus.Update;

public class UpdateMenuItemCommand : IRequest<ErrorOr<Unit>>
{
    public string MenuId { get; set; } = string.Empty;
    public string SectionId { get; set; } = string.Empty;
    public string ItemId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}
public class UpdateMenuItemCommandHandler : IRequestHandler<UpdateMenuItemCommand, ErrorOr<Unit>>
{
    private readonly IMenuRepository _menuRepository;

    public UpdateMenuItemCommandHandler(IMenuRepository menuRepository)
    {
        _menuRepository = menuRepository;
    }

    public async Task<ErrorOr<Unit>> Handle(UpdateMenuItemCommand request, CancellationToken cancellationToken)
    {
        var menu = await _menuRepository.GetByIdAsync(MenuId.Create(request.MenuId));
        if (menu is null) return Error.NotFound($"Menu with ID {request.MenuId} not found.");

        var section = menu.Sections.FirstOrDefault(sec => sec.Id == MenuSectionId.Create(request.SectionId));
        if (section is null) return Error.NotFound($"Section with ID {request.SectionId} not found.");

        var item = section.Items.FirstOrDefault(i => i.Id == MenuItemId.Create(request.ItemId));
        if (item is null) return Error.NotFound($"Item with ID {request.ItemId} not found.");

        // Update item details
        //item.Update(request.Name, request.Description);

        await _menuRepository.UpdateAsync(menu);

        return Unit.Value;
    }
}

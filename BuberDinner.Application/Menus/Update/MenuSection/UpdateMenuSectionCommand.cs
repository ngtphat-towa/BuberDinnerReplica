using MediatR;
using ErrorOr;
using BuberDinner.Application.Interfaces.Repositories;
using BuberDinner.Domain.Menus.ValueObjects;

namespace BuberDinner.Application.Menus.Update;
public class UpdateMenuSectionCommand : IRequest<ErrorOr<Unit>>
{
    public string MenuId { get; set; } = string.Empty;
    public string SectionId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}
public class UpdateMenuSectionCommandHandler : IRequestHandler<UpdateMenuSectionCommand, ErrorOr<Unit>>
{
    private readonly IMenuRepository _menuRepository;

    public UpdateMenuSectionCommandHandler(IMenuRepository menuRepository)
    {
        _menuRepository = menuRepository;
    }

    public async Task<ErrorOr<Unit>> Handle(UpdateMenuSectionCommand request, CancellationToken cancellationToken)
    {
        var menu = await _menuRepository.GetByIdAsync(MenuId.GetId(request.MenuId));
        if (menu is null) return Error.NotFound($"Menu with ID {request.MenuId} not found.");

        var section = menu.Sections.FirstOrDefault(sec => sec.Id == MenuSectionId.GetId(request.SectionId));
        if (section is null) return Error.NotFound($"Section with ID {request.SectionId} not found.");

        // Update section details
        //section.Update(request.Name, request.Description);

        await _menuRepository.UpdateAsync(menu);

        return Unit.Value;
    }
}
using BuberDinner.Application.Interfaces.Repositories;
using BuberDinner.Domain.Menus.ValueObjects;

using ErrorOr;
using MediatR;

namespace BuberDinner.Application.Menus.Delete;

public record DeleteMenuCommand : IRequest<ErrorOr<Unit>>
{
    public string MenuId { get; set; } = string.Empty;
}

public class DeleteMenuCommandHandler : IRequestHandler<DeleteMenuCommand, ErrorOr<Unit>>
{
    private readonly IMenuRepository _menuRepository;

    public DeleteMenuCommandHandler(IMenuRepository menuRepository)
    {
        _menuRepository = menuRepository;
    }

    public async Task<ErrorOr<Unit>> Handle(DeleteMenuCommand request, CancellationToken cancellationToken)
    {
        var menu = await _menuRepository.GetByIdAsync(MenuId.GetId(request.MenuId));
        if (menu is null) return Error.NotFound($"Menu with ID {request.MenuId} not found.");

        await _menuRepository.DeleteAsync(menu);

        return Unit.Value;
    }
}
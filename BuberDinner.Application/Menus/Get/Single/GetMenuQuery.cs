using BuberDinner.Application.Interfaces.Repositories;
using BuberDinner.Application.Menus.Delete;
using BuberDinner.Domain.Menus;
using BuberDinner.Domain.Menus.ValueObjects;

using ErrorOr;

using FluentValidation;

using MediatR;

namespace BuberDinner.Application.Menus.Get;

public class GetMenuQuery : IRequest<ErrorOr<Menu>>
{
    public string MenuId { get; set; } = string.Empty;
}

public class DeleteMenuCommandValidator : AbstractValidator<DeleteMenuCommand>
{
    public DeleteMenuCommandValidator()
    {
        RuleFor(cmd => cmd.MenuId).NotEmpty().WithMessage("Menu ID is required.");
    }
}
public class GetMenuQueryHandler : IRequestHandler<GetMenuQuery, ErrorOr<Menu>>
{
    private readonly IMenuRepository _menuRepository;

    public GetMenuQueryHandler(IMenuRepository menuRepository)
    {
        _menuRepository = menuRepository;
    }

    public async Task<ErrorOr<Menu>> Handle(GetMenuQuery request, CancellationToken cancellationToken)
    {
        var menu = await _menuRepository.GetByIdAsync(MenuId.GetId(request.MenuId));

        if (menu is null) return Error.NotFound($"Menu with ID {request.MenuId} not found.");

        return menu;
    }
}
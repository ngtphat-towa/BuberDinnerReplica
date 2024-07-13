using BuberDinner.Application.Menus.Delete;
using BuberDinner.Domain.Menus;
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

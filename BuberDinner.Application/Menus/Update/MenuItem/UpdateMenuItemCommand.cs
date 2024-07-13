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

public class UpdateMenuSectionCommandValidator : AbstractValidator<UpdateMenuItemCommand>
{
    public UpdateMenuSectionCommandValidator()
    {
        RuleFor(cmd => cmd.MenuId).NotEmpty().WithMessage("Menu ID is required.");
        RuleFor(cmd => cmd.SectionId).NotEmpty().WithMessage("Section ID is required.");
        RuleFor(cmd => cmd.Name).NotEmpty().WithMessage("Section name is required.");
        RuleFor(cmd => cmd.Description).NotEmpty().WithMessage("Section description is required.");
    }
}


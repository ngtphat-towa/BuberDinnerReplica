using FluentValidation;

namespace BuberDinner.Application.Menus.Update;

public class UpdateMenuItemCommandValidator : AbstractValidator<UpdateMenuItemCommand>
{
    public UpdateMenuItemCommandValidator()
    {
        RuleFor(cmd => cmd.MenuId).NotEmpty().WithMessage("Menu ID is required.");
        RuleFor(cmd => cmd.SectionId).NotEmpty().WithMessage("Section ID is required.");
        RuleFor(cmd => cmd.ItemId).NotEmpty().WithMessage("Item ID is required.");
        RuleFor(cmd => cmd.Name).NotEmpty().WithMessage("Item name is required.");
        RuleFor(cmd => cmd.Description).NotEmpty().WithMessage("Item description is required.");
    }
}

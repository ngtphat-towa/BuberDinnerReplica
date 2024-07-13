using FluentValidation;

namespace BuberDinner.Application.Menus.Update;

public class UpdateMenuSectionCommandValidator : AbstractValidator<UpdateMenuSectionCommand>
{
    public UpdateMenuSectionCommandValidator()
    {
        RuleFor(cmd => cmd.MenuId).NotEmpty().WithMessage("Menu ID is required.");
        RuleFor(cmd => cmd.SectionId).NotEmpty().WithMessage("Section ID is required.");
        RuleFor(cmd => cmd.Name).NotEmpty().WithMessage("Section name is required.");
        RuleFor(cmd => cmd.Description).NotEmpty().WithMessage("Section description is required.");
    }
}
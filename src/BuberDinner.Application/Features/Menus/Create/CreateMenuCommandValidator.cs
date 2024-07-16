using FluentValidation;

namespace BuberDinner.Application.Features.Menus.Create;

public class CreateMenuCommandValidator : AbstractValidator<CreateMenuCommand>
{
    public CreateMenuCommandValidator()
    {
        RuleFor(cmd => cmd.HostId)
            .NotEmpty().WithMessage("Host ID is required.");

        RuleFor(cmd => cmd.Name)
            .NotEmpty().WithMessage("Menu name is required.");

        RuleFor(cmd => cmd.Description)
            .NotEmpty().WithMessage("Menu description is required.");

        RuleForEach(cmd => cmd.Sections)
            .SetValidator(new MenuSectionRequestValidator());
    }
}

public class MenuSectionRequestValidator : AbstractValidator<MenuSectionCommand>
{
    public MenuSectionRequestValidator()
    {
        RuleFor(sec => sec.Name)
            .NotEmpty().WithMessage("Section name is required.");

        RuleFor(sec => sec.Description)
            .NotEmpty().WithMessage("Section description is required.");

        RuleForEach(sec => sec.Items)
            .SetValidator(new MenuItemRequestValidator());
    }
}

public class MenuItemRequestValidator : AbstractValidator<MenuItemCommand>
{
    public MenuItemRequestValidator()
    {
        RuleFor(item => item.Name)
            .NotEmpty().WithMessage("Item name is required.");

        RuleFor(item => item.Description)
            .NotEmpty().WithMessage("Item description is required.");
    }
}

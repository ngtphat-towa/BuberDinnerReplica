using FluentValidation;

namespace BuberDinner.Application.Menus.Get;


public class GetMenuQueryValidator : AbstractValidator<GetMenuQuery>
{
    public GetMenuQueryValidator()
    {
        RuleFor(cmd => cmd.MenuId).NotEmpty().WithMessage("Menu ID is required.");
    }
}

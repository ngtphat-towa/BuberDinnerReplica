using FluentValidation;

namespace BuberDinner.Application.Features.Menus.Get.All;

public class GetAllMenusQueryValidator : AbstractValidator<GetAllMenusQuery>
{
    public GetAllMenusQueryValidator()
    {
        RuleFor(query => query.PageNumber)
            .GreaterThan(0).WithMessage("Page number must be greater than zero.");

        RuleFor(query => query.PageSize)
            .GreaterThan(0).WithMessage("Page size must be greater than zero.")
            .LessThanOrEqualTo(100).WithMessage("Page size must be less than or equal to 100.");
    }
}
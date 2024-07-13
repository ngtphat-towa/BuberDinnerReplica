using ErrorOr;
using MediatR;

namespace BuberDinner.Application.Menus.Delete;

public record DeleteMenuCommand : IRequest<ErrorOr<Unit>>
{
    public string MenuId { get; set; } = string.Empty;
}
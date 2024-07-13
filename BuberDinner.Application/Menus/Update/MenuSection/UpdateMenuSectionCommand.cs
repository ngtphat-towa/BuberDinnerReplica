using MediatR;
using ErrorOr;

namespace BuberDinner.Application.Menus.Update;
public class UpdateMenuSectionCommand : IRequest<ErrorOr<Unit>>
{
    public string MenuId { get; set; } = string.Empty;
    public string SectionId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}

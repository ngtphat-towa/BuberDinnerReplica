using BuberDinner.Domain.Menus;
using BuberDinner.Domain.Wrapper;

using ErrorOr;

using MediatR;

namespace BuberDinner.Application.Menus.GetAll;

public class GetAllMenusQuery : IRequest<ErrorOr<PagedList<Menu>>>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}

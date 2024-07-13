using BuberDinner.Application.Interfaces.Repositories;
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

public class GetAllMenusQueryHandler : IRequestHandler<GetAllMenusQuery, ErrorOr<PagedList<Menu>>>
{
    private readonly IMenuRepository _menuRepository;

    public GetAllMenusQueryHandler(IMenuRepository menuRepository)
    {
        _menuRepository = menuRepository;
    }

    public async Task<ErrorOr<PagedList<Menu>>> Handle(GetAllMenusQuery request, CancellationToken cancellationToken)
    {
        var menus = await _menuRepository.GetAllAsync(request.PageNumber, request.PageSize);
        return menus;
    }
}
using BuberDinner.Application.Common.Wrappers;
using BuberDinner.Application.Menus.Create;
using BuberDinner.Application.Menus.Delete;
using BuberDinner.Application.Menus.Get;
using BuberDinner.Application.Menus.GetAll;
using BuberDinner.Application.Menus.Update;
using BuberDinner.Contracts.Menu;
using BuberDinner.Domain.Wrapper;

using Mapster;

using MapsterMapper;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace BuberDinner.Api.Controllers;

[ProducesResponseType(StatusCodes.Status404NotFound)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
public class MenusController : ApiControllerBase
{
    private readonly IMapper _mapper;
    private readonly ISender _mediator;

    public MenusController(IMapper mapper, ISender sender)
    {
        _mapper = mapper;
        _mediator = sender;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateMenu([FromBody] CreateMenuRequest request)
    {
        var command = _mapper.Map<CreateMenuCommand>(request);
        var result = await _mediator.Send(command);
        return result.Match(
           menu => Ok(_mapper.Map<MenuResponse>(menu)),
           errors => Problem(errors));
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllMenus([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        var query = new GetAllMenusQuery { PageNumber = pageNumber, PageSize = pageSize };
        var result = await _mediator.Send(query);

        return result.Match(
          pagedList =>Ok(new PaginationResponse<MenuResponse>
          {
              Data = pagedList.Select(item => _mapper.Map<MenuResponse>(item)).ToList(),
              Meta = new PagedResponseMeta
              {
                  CurrentPage = pagedList.CurrentPage,
                  PageSize = pagedList.PageSize,
                  TotalCount = pagedList.TotalCount,
                  TotalPages = pagedList.TotalPages,
                  HasPrevious = pagedList.HasPrevious,
                  HasNext = pagedList.HasNext
              }
          }),
          errors => Problem(errors)
      );
    }

    [HttpGet("{menuId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetMenu(string menuId)
    {
        var query = new GetMenuQuery { MenuId = menuId };
        var result = await _mediator.Send(query);

        return result.Match(
            menu => Ok(_mapper.Map<MenuResponse>(menu)),
            errors => Problem(errors));
    }

    [HttpPut("{menuId}/section/{sectionId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> UpdateMenuSection(
        string menuId,
        string sectionId,
        [FromBody] UpdateMenuSectionRequest request)
    {
        var command = _mapper.Map<UpdateMenuSectionCommand>(request);
        command.MenuId = menuId;
        command.SectionId = sectionId;

        var result = await _mediator.Send(command);
        return result.Match(
            _ => NoContent(),
            errors => Problem(errors));
    }

    [HttpPut("{menuId}/section/{sectionId}/item/{itemId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> UpdateMenuItem(
        string menuId,
        string sectionId,
        string itemId,
        [FromBody] UpdateMenuItemRequest request)
    {
        var command = _mapper.Map<UpdateMenuItemCommand>(request);
        command.MenuId = menuId;
        command.SectionId = sectionId;
        command.ItemId = itemId;

        var result = await _mediator.Send(command);
        return result.Match(
            _ => NoContent(),
            errors => Problem(errors));
    }

    [HttpDelete("{menuId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteMenu(string menuId)
    {
        var command = new DeleteMenuCommand { MenuId = menuId };
        var result = await _mediator.Send(command);
        return result.Match(
            _ => NoContent(),
            errors => Problem(errors));
    }
}

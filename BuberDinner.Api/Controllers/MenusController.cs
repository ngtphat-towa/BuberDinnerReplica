using BuberDinner.Application.Menus.Create;
using BuberDinner.Contracts.Menu;

using MapsterMapper;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace BuberDinner.Api.Controllers;

[ProducesResponseType(StatusCodes.Status401Unauthorized)]
[ProducesResponseType(StatusCodes.Status200OK)]
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
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateMenu([FromBody] CreateMenuRequest request)
    {
        var command = _mapper.Map<CreateMenuCommand>(request);
        var result = await _mediator.Send(command);
        return result.Match(
           menu => Ok(_mapper.Map<MenuResponse>(menu)),
           errors => Problem(errors));
    }

    [HttpGet("{menuId}")]
    public async Task<IActionResult> GetMenu(string menuId)
    {
        await Task.CompletedTask;

        return NoContent();

    }

    [HttpPut("{menuId}/section/{sectionId}")]
    public async Task<IActionResult> UpdateMenuSection(string menuId, string sectionId, [FromBody] UpdateMenuSectionRequest request)
    {
        await Task.CompletedTask;

        return NoContent();

    }

    [HttpPut("{menuId}/section/{sectionId}/item/{itemId}")]
    public async Task<IActionResult> UpdateMenuItem(string menuId, string sectionId, string itemId, [FromBody] UpdateMenuItemRequest request)
    {
        await Task.CompletedTask;

        return NoContent();

    }

    [HttpDelete("{menuId}")]
    public async Task<IActionResult> DeleteMenu(string menuId)
    {
        await Task.CompletedTask;

        return NoContent();
    }

}

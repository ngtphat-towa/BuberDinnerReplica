using BuberDinner.Contracts.Menu;
using BuberDinner.Domain.Menu.ValueObjects;

using Microsoft.AspNetCore.Mvc;

namespace BuberDinner.Api.Controllers;


public class MenusController : ApiControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateMenu([FromBody] CreateMenuRequest request)
    {
        await Task.CompletedTask;
        return NoContent();

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

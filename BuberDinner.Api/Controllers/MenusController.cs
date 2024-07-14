using BuberDinner.Application.Common.Wrappers;
using BuberDinner.Application.Features.Menus.Create;
using BuberDinner.Application.Features.Menus.Delete;
using BuberDinner.Application.Features.Menus.Get.All;
using BuberDinner.Application.Features.Menus.Get.Single;
using BuberDinner.Application.Features.Menus.Update.MenuItem;
using BuberDinner.Application.Features.Menus.Update.MenuSection;
using BuberDinner.Contracts.Menu;

using MapsterMapper;

using MediatR;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BuberDinner.Api.Controllers
{
    /// <summary>
    /// Controller for managing restaurant menus.
    /// </summary>
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class MenusController : ApiControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ISender _mediator;

        public MenusController(IMapper mapper, ISender mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        /// <summary>
        /// Creates a new menu.
        /// </summary>
        /// <param name="request">Request object containing menu details.</param>
        /// <returns>Returns the created menu upon success.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(MenuResponse), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateMenu([FromBody] CreateMenuRequest request)
        {
            var command = _mapper.Map<CreateMenuCommand>(request);
            var result = await _mediator.Send(command);

            return result.Match(
                menu => CreatedAtAction(nameof(GetMenu), new { menuId = menu.Id }, _mapper.Map<MenuResponse>(menu)),
                errors => Problem(errors)
            );
        }

        /// <summary>
        /// Retrieves all menus with pagination support.
        /// </summary>
        /// <param name="pageNumber">Page number for pagination (default is 1).</param>
        /// <param name="pageSize">Number of items per page (default is 10).</param>
        /// <returns>Returns a paginated list of menus.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(PaginationResponse<MenuResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllMenus([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var query = new GetAllMenusQuery { PageNumber = pageNumber, PageSize = pageSize };
            var result = await _mediator.Send(query);

            return result.Match(
                pagedList => Ok(new PaginationResponse<MenuResponse>
                {
                    Data = pagedList.Select(menu => _mapper.Map<MenuResponse>(menu)).ToList(),
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

        /// <summary>
        /// Retrieves a specific menu by its ID.
        /// </summary>
        /// <param name="menuId">ID of the menu to retrieve.</param>
        /// <returns>Returns the menu details if found.</returns>
        [HttpGet("{menuId}")]
        [ProducesResponseType(typeof(MenuResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetMenu(string menuId)
        {
            var query = new GetMenuQuery { MenuId = menuId };
            var result = await _mediator.Send(query);

            return result.Match(
                menu => Ok(_mapper.Map<MenuResponse>(menu)),
                errors => Problem(errors)
            );
        }

        /// <summary>
        /// Updates a section within a menu.
        /// </summary>
        /// <param name="menuId">ID of the menu containing the section.</param>
        /// <param name="sectionId">ID of the section to update.</param>
        /// <param name="request">Request object containing updated section details.</param>
        /// <returns>Returns no content upon successful update.</returns>
        [HttpPut("{menuId}/section/{sectionId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
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
                errors => Problem(errors)
            );
        }

        /// <summary>
        /// Updates a menu item within a specific section of a menu.
        /// </summary>
        /// <param name="menuId">ID of the menu containing the section.</param>
        /// <param name="sectionId">ID of the section containing the item.</param>
        /// <param name="itemId">ID of the item to update.</param>
        /// <param name="request">Request object containing updated item details.</param>
        /// <returns>Returns no content upon successful update.</returns>
        [HttpPut("{menuId}/section/{sectionId}/item/{itemId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
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
                errors => Problem(errors)
            );
        }

        /// <summary>
        /// Deletes a menu by its ID.
        /// </summary>
        /// <param name="menuId">ID of the menu to delete.</param>
        /// <returns>Returns no content upon successful deletion.</returns>
        [HttpDelete("{menuId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteMenu(string menuId)
        {
            var command = new DeleteMenuCommand { MenuId = menuId };
            var result = await _mediator.Send(command);

            return result.Match(
                _ => NoContent(),
                errors => Problem(errors)
            );
        }
    }
}

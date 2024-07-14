namespace BuberDinner.Contracts.Menu;

public record CreateMenuRequest(
    string Name,
    string Description,
    Guid HostId,
    List<MenuSectionTransfer> Sections);

public record MenuSectionTransfer(
    string Name,
    string Description,
    List<MenuItemTransfer> Items);

public record MenuItemTransfer(
    string Name,
    string Description);

public record UpdateMenuSectionRequest(
    string Name,
    string Description);

public record UpdateMenuItemRequest(
    string Name,
    string Description);


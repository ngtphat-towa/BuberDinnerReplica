using BuberDinner.Application.Features.Menus.Create;
using BuberDinner.Application.UnitTests.TestUtils.Constants;

namespace BuberDinner.Application.UnitTests.Menus.Commands.TestUtils;

public class CreateMenuCommandUtils
{
    public static CreateMenuCommand CreateCommand(
        List<MenuSectionCommand>? sections = null) =>
        new CreateMenuCommand(
            FixtureConstants.Host.Id.Value,
            FixtureConstants.Menus.Name,
            FixtureConstants.Menus.Description,
            sections ?? CreateMenuSectionCommand());

    public static List<MenuSectionCommand> CreateMenuSectionCommand(
        int sectionsCount = 1,
        List<MenuItemCommand>? items = null) =>
            Enumerable.Range(0, sectionsCount)
                .Select(index => new MenuSectionCommand(
                    FixtureConstants.Menus.SectionDescriptionFromIndex(index),
                    FixtureConstants.Menus.SectionDescriptionFromIndex(index),
                    items ?? CreateMenuItemCommand(sectionsCount)))
                .ToList();

    public static List<MenuItemCommand> CreateMenuItemCommand(int itemsCount = 1) =>
        Enumerable.Range(0, itemsCount)
            .Select(index => new MenuItemCommand(
                FixtureConstants.Menus.ItemNameFromIndex(index),
                FixtureConstants.Menus.ItemDescriptionFromIndex(index)))
            .ToList();
}
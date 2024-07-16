using BuberDinner.Application.Features.Menus.Create;
using BuberDinner.Application.Interfaces.Repositories;
using BuberDinner.Application.UnitTests.Menus.Commands.TestUtils;
using BuberDinner.Application.UnitTests.TestUtils.Menus.Extensions;
using BuberDinner.Domain.Menus;

using FluentAssertions;

using Moq;

namespace BuberDinner.Application.UnitTests.Features.Menus.Commands.CreateMenu
{
    public class CreateMenuCommandHandlerTests
    {
        private readonly Mock<IMenuRepository> _mockMenuRepository;
        private readonly CreateMenuCommandHandler _handler;

        public CreateMenuCommandHandlerTests()
        {
            _mockMenuRepository = new Mock<IMenuRepository>();
            _handler = new CreateMenuCommandHandler(_mockMenuRepository.Object);
        }

        // T1: SUT - logical component we're testing (class, method)
        // T2: Scenario - what we're testing
        // T3: Expected outcome - where we expect the logical component to do
        [Theory]
        [MemberData(nameof(ValidCreateMenuCommands))]
        public async Task HandleCreateMenuCommand_WhenMenuIsValid_ShouldCreateAndReturnMenu(
            CreateMenuCommand command)
        {
            // Act
            var result = await _handler.Handle(command, default);

            // Assert
            result.IsError.Should().BeFalse();
            result.Value.ValidateCreatedFrom(command);
            _mockMenuRepository.Verify(x => x.AddAsync(result.Value), Times.Once);
        }

        public static IEnumerable<object[]> ValidCreateMenuCommands()
        {
            yield return new[] { CreateMenuCommandUtils.CreateCommand() };

            yield return new[]
            {
            CreateMenuCommandUtils.CreateCommand(
                sections: CreateMenuCommandUtils.CreateMenuSectionCommand(3)),
        };

            yield return new[]
            {
            CreateMenuCommandUtils.CreateCommand(
                sections: CreateMenuCommandUtils.CreateMenuSectionCommand(
                    sectionsCount: 3,
                    items: CreateMenuCommandUtils.CreateMenuItemCommand(5))),
        };
        }
    }
}

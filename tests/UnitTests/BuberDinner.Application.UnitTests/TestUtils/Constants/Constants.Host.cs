using BuberDinner.Domain.Host.ValueObjects;

namespace BuberDinner.Application.UnitTests.TestUtils.Constants;

public static partial class FixtureConstants
{
    public static class Host
    {
        public static readonly HostId Id = HostId.CreateUnique();
    }
}
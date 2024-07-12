using BuberDinner.Domain.Common.Models;

namespace BuberDinner.Domain.Host.ValueObjects;

public sealed class HostId : ValueObject
{
    // TODO: replace for string for testing only
    public string Value { get; }

    private HostId(string value)
    {
        Value = value;
    }

    public static HostId CreateUnique()
    {
        return new(Guid.NewGuid().ToString());
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static HostId Create(string value)
    {
        return new(value);
    }
}
namespace BuberDinner.Domain.Common.Models;
/// <summary>
/// Base class for immutable value objects in Domain-Driven Design (DDD).
/// Value objects are defined by the combination of their attributes (values).
/// </summary>
public abstract class ValueObject : IEquatable<ValueObject>
{
    /// <summary>
    /// Gets the components that define equality for this value object.
    /// Subclasses must implement this method to specify equality components.
    /// </summary>
    /// <returns>An enumerable of objects representing equality components.</returns>
    public abstract IEnumerable<object> GetEqualityComponents();

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (obj is null || obj.GetType() != GetType())
        {
            return false;
        }

        var valueObject = (ValueObject)obj;
        return GetEqualityComponents().SequenceEqual(valueObject.GetEqualityComponents());
    }

    /// <inheritdoc />
    public static bool operator ==(ValueObject left, ValueObject right)
    {
        return Equals(left, right);
    }

    /// <inheritdoc />
    public static bool operator !=(ValueObject left, ValueObject right)
    {
        return !Equals(left, right);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return GetEqualityComponents()
                .Select(x => x?.GetHashCode() ?? 0)
                .Aggregate((x, y) => x ^ y);
    }

    /// <inheritdoc />
    public bool Equals(ValueObject? other)
    {
        return Equals((object?)other);
    }
}

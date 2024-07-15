namespace BuberDinner.Domain.Common.Models;

/// <summary>
/// Abstract base class for defining an identity of an aggregate root in DDD.
/// </summary>
/// <typeparam name="TId">Type of the identity value.</typeparam>
public abstract class AggregateRootId<TId> : ValueObject
{
    /// <summary>
    /// Gets the value of the aggregate root identity.
    /// </summary>
    public abstract TId Value { get; protected set; }
}
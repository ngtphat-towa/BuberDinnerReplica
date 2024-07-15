namespace BuberDinner.Domain.Common.Models;

/// <summary>
/// Base class representing an aggregate root in Domain-Driven Design (DDD).
/// Aggregate roots are fundamental entities that serve as the root of an aggregate.
/// </summary>
/// <typeparam name="TId">Type of the aggregate root's identity.</typeparam>
/// <typeparam name="TIdType">Type of the value within the identity.</typeparam>
public abstract class AggregateRoot<TId, TIdType> : Entity<TId> where TId : AggregateRootId<TIdType>
{
    /// <summary>
    /// Gets the unique identity of the aggregate root.
    /// </summary>
    public new AggregateRootId<TIdType> Id { get; protected set; }

    /// <summary>
    /// Gets the value of the aggregate root's identity.
    /// </summary>
    public TIdType IdValue => Id.Value;

    /// <summary>
    /// Initializes a new instance of the <see cref="AggregateRoot{TId, TIdType}"/> class with the specified identity.
    /// </summary>
    /// <param name="id">The identity of the aggregate root.</param>
    protected AggregateRoot(TId id) : base(id)
    {
        Id = id;
    }

    /// <summary>
    /// Default constructor required for ORM and serialization.
    /// </summary>
    protected AggregateRoot() : base()
    {
        Id = default!;
    }
}

namespace BuberDinner.Domain.Common.Models;

/// <summary>
/// Base class for aggregate roots in Domain-Driven Design (DDD).
/// Aggregate roots are special entities that act as the root of an aggregate.
/// </summary>
/// <typeparam name="TId">The type of the aggregate root's identity.</typeparam>
public abstract class AggregateRoot<TId, TIdType> : Entity<TId> where TId : AggregateRootId<TIdType>
{
    public new AggregateRootId<TIdType> Id { get; protected set; }
    /// <summary>
    /// Initializes a new instance of the <see cref="AggregateRoot{TId}"/> class with the specified Id.
    /// </summary>
    /// <param name="id">The identity of the aggregate root.</param>
    protected AggregateRoot(TId id) : base(id)
    {
        Id = id;
    }
    protected AggregateRoot() : base()
    {
        Id = default!;
    }
}

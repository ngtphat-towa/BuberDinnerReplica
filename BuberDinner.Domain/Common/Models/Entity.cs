namespace BuberDinner.Domain.Common.Models;

/// <summary>
/// Base class for entities in Domain-Driven Design (DDD).
/// Entities are objects with a unique identity represented by the Id property.
/// </summary>
/// <typeparam name="TId">The type of the entity's identity.</typeparam>
public abstract class Entity<TId> : IEquatable<Entity<TId>>
    where TId : notnull
{
    /// <summary>
    /// Gets the unique identity of the entity.
    /// </summary>
    public TId Id { get; protected set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Entity{TId}"/> class with the specified Id.
    /// </summary>
    /// <param name="id">The identity of the entity.</param>
    protected Entity(TId id)
    {
        Id = id;
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return obj is Entity<TId> entity && Id.Equals(entity.Id);
    }

    /// <inheritdoc />
    public bool Equals(Entity<TId>? other)
    {
        return Equals((object?)other);
    }

    /// <inheritdoc />
    public static bool operator ==(Entity<TId> left, Entity<TId> right)
    {
        return Equals(left, right);
    }

    /// <inheritdoc />
    public static bool operator !=(Entity<TId> left, Entity<TId> right)
    {
        return !Equals(left, right);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}

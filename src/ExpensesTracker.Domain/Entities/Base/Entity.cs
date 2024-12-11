using System.Diagnostics.CodeAnalysis;

namespace ExpensesTracker.Domain.Entities.Base;

public abstract class Entity : IEquatable<Entity>
{
    public int Id { get; private init; }

    protected Entity(int id)
    {
        Id = id;
    }

    protected Entity()
    {

    }

    public static bool operator ==(Entity? first, Entity? second)
    {
        return first is not null && second is not null && first.Equals(second);
    }

    public static bool operator !=(Entity? first, Entity? second)
    {
        return !(first == second);
    }

    public virtual bool Equals(Entity? other)
    {
        if (IsNullOrDifferentType(other))
        {
            return false;
        }

        return Id == other.Id;
    }

    public override bool Equals(object? obj)
    {
        if (IsNullOrDifferentType(obj))
        {
            return false;
        }

        if (obj is not Entity entity)
        {
            return false;
        }

        return entity.Id == Id;
    }

    private bool IsNullOrDifferentType([NotNullWhen(false)] object? obj)
    {

        if (obj is null)
        {
            return true;
        }

        return obj.GetType() != GetType();

    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}
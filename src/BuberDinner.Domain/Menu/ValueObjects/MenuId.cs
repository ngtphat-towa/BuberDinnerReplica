using BuberDinner.Domain.Common.Models;

namespace BuberDinner.Domain.Menus.ValueObjects;

public sealed class MenuId : AggregateRootId<Guid>
{
    public override Guid Value { get ; protected set ; }

    private MenuId(Guid value)
    {
        Value = value;
    }


    public static MenuId Create()
    {
        return new(Guid.NewGuid());
    }
    public static MenuId Create(Guid id)
    {
        return new(id);
    }

    public static MenuId Create(string id)
    {
        return new(Guid.Parse(id));
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
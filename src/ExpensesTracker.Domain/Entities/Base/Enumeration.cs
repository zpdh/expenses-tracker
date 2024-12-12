using System.Reflection;

namespace ExpensesTracker.Domain.Entities.Base;

public abstract class Enumeration<TEnum> : IEquatable<Enumeration<TEnum>> where TEnum : Enumeration<TEnum>
{
    private static readonly Dictionary<int, TEnum> GetEnumerations = CreateEnumerations();

    public int Id { get; protected init; }
    public string Name { get; protected init; }

    protected Enumeration(int id, string name)
    {
        Id = id;
        Name = name;
    }

    private Enumeration()
    {

    }

    public static TEnum? FromValue(int value)
    {
        return GetEnumerations.GetValueOrDefault(value);
    }

    public static TEnum? FromName(string name)
    {
        return GetEnumerations.Values.SingleOrDefault(enumeration => enumeration.Name == name);
    }

    public static IReadOnlyCollection<TEnum> GetValues()
    {
        return GetEnumerations.Values.ToList();
    }

    private static Dictionary<int, TEnum> CreateEnumerations()
    {
        var enumerationType = typeof(TEnum);

        var fields = enumerationType
            .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
            .Where(fieldInfo => enumerationType.IsAssignableFrom(fieldInfo.FieldType))
            .Select(fieldInfo => (TEnum)fieldInfo.GetValue(default)!);

        return fields.ToDictionary(type => type.Id);
    }

    public bool Equals(Enumeration<TEnum>? other)
    {
        if (other is null)
        {
            return false;
        }

        return GetType() == other.GetType() && Id == other.Id;
    }

    public override bool Equals(object? obj)
    {
        return obj is Enumeration<TEnum> other && Equals(other);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    public override string ToString()
    {
        return Name;
    }
}
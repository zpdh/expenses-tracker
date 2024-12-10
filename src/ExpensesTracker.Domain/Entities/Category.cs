using System.Text.Json.Serialization;

namespace ExpensesTracker.Domain.Entities;

public sealed class Category : Entity
{
    public string Name { get; set; } = string.Empty;
    [JsonIgnore] public IEnumerable<Expense> Expenses { get; set; } = default!;

    public Category(string name)
    {
        Name = name;
    }

    private Category()
    {

    }

    public static Category Create(string name) => new(name);
}
using System.Text.Json.Serialization;
using ExpensesTracker.Domain.Entities.Base;

namespace ExpensesTracker.Domain.Entities;

/// <summary>
/// Category to be used to group <see cref="Expenses"/>.
/// These act like enums, but are stored on the database so they can be dynamically updated throughout the application.
/// The default permissions needed to add a category to the database are administrator permissions.
/// </summary>
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
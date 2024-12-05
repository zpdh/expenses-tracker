using System.Text.Json.Serialization;

namespace ExpensesTracker.Domain.Entities;

public sealed class Category : Entity
{
    public string Name { get; set; } = string.Empty;
    [JsonIgnore]
    public IEnumerable<Expense> Expenses { get; set; } = default!;
}
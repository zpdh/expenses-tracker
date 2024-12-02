using System.Text.Json.Serialization;

namespace ExpensesTracker.Domain.Entities;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    [JsonIgnore]
    public IEnumerable<Expense> Expenses { get; set; } = default!;
}
using System.Text.Json.Serialization;

namespace ExpensesTracker.Domain.Entities;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    [JsonIgnore]
    public IEnumerable<Expense> Expenses { get; set; } = default!;
}
using System.Text.Json.Serialization;

namespace ExpensesTracker.Domain.Entities;

public class Expense
{
    public int Id { get; set; }
    public int CategoryId { get; set; }
    public int UserId { get; set; }
    public string Name { get; set; } = string.Empty;
    public double Price { get; set; }
    public DateTime InsertionDate { get; set; } = DateTime.UtcNow;
    [JsonIgnore]
    public User User { get; set; }
    [JsonIgnore]
    public Category Category { get; set; }
}
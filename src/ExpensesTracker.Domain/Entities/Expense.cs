using System.Text.Json.Serialization;
using ExpensesTracker.Domain.Dtos;
using ExpensesTracker.Domain.Entities.Base;

namespace ExpensesTracker.Domain.Entities;

public sealed class Expense : Entity
{
    public int CategoryId { get; set; }
    public int UserId { get; set; }
    public string Name { get; set; } = string.Empty;
    public double Price { get; set; }
    public DateTime InsertionDate { get; set; } = DateTime.UtcNow;
    [JsonIgnore] public User User { get; set; }
    [JsonIgnore] public Category Category { get; set; }

    private Expense(int categoryId, int userId, string name, double price)
    {
        CategoryId = categoryId;
        UserId = userId;
        Name = name;
        Price = price;
    }

    private Expense()
    {

    }

    public static Expense Create(int categoryId, int userId, string name, double price)
    {
        return new Expense(categoryId, userId, name, price);
    }

    public static Expense Create(AddExpenseDto dto)
    {
        return new Expense(dto.CategoryId, dto.UserId, dto.Name, dto.Price);
    }
}
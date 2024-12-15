using System.Text.Json.Serialization;
using ExpensesTracker.Domain.Entities.Base;

namespace ExpensesTracker.Domain.Entities;

public sealed class User : Entity
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public ICollection<Role> Roles { get; set; }
    [JsonIgnore] public IEnumerable<Expense> Expenses { get; set; } = [];

    private User(string name, string email, string password)
    {
        Name = name;
        Email = email;
        Password = password;
    }

    private User()
    {
    }

    public static User Create(string name, string email, string password) => new(name, email, password);
}
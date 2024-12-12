using ExpensesTracker.Domain.Entities.Base;

namespace ExpensesTracker.Domain.Entities;

public class Role : Enumeration<Role>
{
    public static readonly Role Registered = new(1, "Registered");
    public IEnumerable<Permission> Permissions { get; set; }
    public IEnumerable<User> Users { get; set; }

    public Role(int id, string name) : base(id, name)
    {
    }
}
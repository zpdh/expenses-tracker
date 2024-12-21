using ExpensesTracker.Domain.Entities.Base;

namespace ExpensesTracker.Domain.Entities;

public class Role : Enumeration<Role>
{
    public static readonly Role Registered = new(1, "Registered");
    public static readonly Role Administrator = new(2, "Administrator");
    public ICollection<Permission> Permissions { get; set; }
    public ICollection<User> Users { get; set; }

    public Role(int id, string name) : base(id, name)
    {
    }
}
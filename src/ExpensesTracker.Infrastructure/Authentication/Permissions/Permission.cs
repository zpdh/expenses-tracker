using ExpensesTracker.Domain.Entities.Base;

namespace ExpensesTracker.Infrastructure.Authentication.Permissions;

public class Permission : Enumeration<Permission>
{
    private Permission(int value, string name) : base(value, name)
    {
    }
}
namespace ExpensesTracker.Domain.Entities;

public class PermissionRole
{
    public int RoleId { get; set; }

    public int PermissionId { get; set; }

    private PermissionRole(Role role, Enums.Permission permission)
    {
        RoleId = role.Id;
        PermissionId = (int)permission;
    }

    private PermissionRole()
    {

    }

    public static PermissionRole Create(Role role, Enums.Permission permission)
    {
        return new PermissionRole(role, permission);
    }
}
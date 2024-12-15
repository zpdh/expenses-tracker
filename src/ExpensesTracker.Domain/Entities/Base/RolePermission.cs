namespace ExpensesTracker.Domain.Entities.Base;

public class RolePermission
{
    public int RoleId { get; set; }

    public int PermissionId { get; set; }

    private RolePermission(Role role, Enums.Permission permission)
    {
        RoleId = role.Id;
        PermissionId = (int)permission;
    }

    private RolePermission()
    {

    }

    public static RolePermission Create(Role role, Enums.Permission permission) => new(role, permission);
}
namespace ExpensesTracker.Infrastructure.Authentication.Permissions;

public interface IPermissionService
{
    Task<HashSet<string>> GetPermissionsAsync(int userId);
}
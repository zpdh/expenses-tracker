using ExpensesTracker.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ExpensesTracker.Infrastructure.Authentication.Permissions;

public class PermissionService : IPermissionService
{
    private readonly DataContext _context;

    public PermissionService(DataContext context)
    {
        _context = context;
    }

    public async Task<HashSet<string>> GetPermissionsAsync(int userId)
    {
        var roles = await _context.Users
            .Include(user => user.Roles)
            .ThenInclude(role => role.Permissions)
            .Where(user => user.Id == userId)
            .Select(user => user.Roles)
            .ToArrayAsync();

        return roles.SelectMany(roleEnumerable => roleEnumerable)
            .SelectMany(role => role.Permissions)
            .Select(permission => permission.Name)
            .ToHashSet();
    }
}
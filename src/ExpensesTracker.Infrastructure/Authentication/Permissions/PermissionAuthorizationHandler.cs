using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.JsonWebTokens;

namespace ExpensesTracker.Infrastructure.Authentication.Permissions;

public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public PermissionAuthorizationHandler(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
    {
        var userId = GetUserId(context);

        if (userId is null)
        {
            return;
        }

        var permissions = await GetPermissionsAsync(userId.Value);

        if (permissions.Contains(requirement.Permission))
        {
            context.Succeed(requirement);
        }
    }

    private static int? GetUserId(AuthorizationHandlerContext context)
    {
        var userId = context.User.Claims.FirstOrDefault(u => u.Type is ClaimTypes.NameIdentifier or JwtRegisteredClaimNames.Sub)?.Value;

        if (!int.TryParse(userId, out var parsedUserId))
        {
            return null;
        }

        return parsedUserId;
    }

    private async Task<HashSet<string>> GetPermissionsAsync(int userId)
    {
        using var scope = _serviceScopeFactory.CreateScope();

        var permissionService = scope.ServiceProvider.GetRequiredService<IPermissionService>();

        return await permissionService.GetPermissionsAsync(userId);
    }

}
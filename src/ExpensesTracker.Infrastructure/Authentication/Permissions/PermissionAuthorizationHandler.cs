using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;
using ExpensesTracker.Infrastructure.Authentication.Jwt;
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

    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
    {
        var permissions = GetPermissions(context);

        if (permissions.Contains(requirement.Permission))
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }

    private static HashSet<string> GetPermissions(AuthorizationHandlerContext context)
    {
        return context.User.Claims
            .Where(claim => claim.Type == CustomClaims.Permissions)
            .Select(claim => claim.Value)
            .ToHashSet();
    }
}
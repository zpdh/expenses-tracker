using Microsoft.AspNetCore.Authorization;

namespace ExpensesTracker.Infrastructure.Authentication.Permissions;

public sealed class HasPermissionAttribute(Permission permission) : AuthorizeAttribute(policy: permission.ToString());
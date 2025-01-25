using ExpensesTracker.Domain.Enums;
using Microsoft.AspNetCore.Authorization;

namespace ExpensesTracker.Infrastructure.Authentication.Permissions;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
public sealed class HasPermissionAttribute(Permission permission) : AuthorizeAttribute(permission.ToString());
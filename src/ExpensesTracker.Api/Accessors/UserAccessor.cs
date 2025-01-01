using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using ExpensesTracker.Domain.Errors.Base;
using ExpensesTracker.Domain.Results;
using Microsoft.IdentityModel.Tokens;

namespace ExpensesTracker.Api.Accessors;

public interface IUserAccessor
{
    int GetRequestingUserId();
}

public class UserAccessor : IUserAccessor
{
    private readonly IHttpContextAccessor _contextAccessor;

    public UserAccessor(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }

    public int GetRequestingUserId()
    {
        var idClaim = _contextAccessor.HttpContext!.User
            .Claims
            .FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)!
            .Value;

        return int.Parse(idClaim);
    }
}
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ExpensesTracker.Domain.Entities;
using ExpensesTracker.Domain.Infrastructure.Tokens;
using ExpensesTracker.Infrastructure.Authentication.Permissions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ExpensesTracker.Infrastructure.Authentication.Jwt;

public sealed class JwtGenerator : IJwtGenerator
{
    private readonly JwtOptions _options;
    private readonly IPermissionService _permissionService;

    public JwtGenerator(IOptions<JwtOptions> options, IPermissionService permissionService)
    {
        _permissionService = permissionService;
        _options = options.Value;
    }

    public async Task<string> GenerateAsync(User user)
    {
        var permissions = await _permissionService.GetPermissionsAsync(user.Id);

        var claims = GenerateClaims(user, permissions);

        var signingCredentials = GenerateSigningCredentials();

        var tokenAsString = GenerateToken(claims, signingCredentials);

        return tokenAsString;
    }

    private static List<Claim> GenerateClaims(User user, HashSet<string> permissions)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email)
        };

        claims.AddRange(permissions.Select(permission => new Claim(CustomClaims.Permissions, permission)));

        return claims;
    }

    private SigningCredentials GenerateSigningCredentials()
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey));

        return new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
    }

    private string GenerateToken(List<Claim> claims, SigningCredentials signingCredentials)
    {
        var token = new JwtSecurityToken(_options.Issuer, _options.Audience, claims, null, DateTime.UtcNow.AddHours(1), signingCredentials);

        var tokenHandler = new JwtSecurityTokenHandler();

        var tokenValue = tokenHandler.WriteToken(token);
        return tokenValue;
    }
}
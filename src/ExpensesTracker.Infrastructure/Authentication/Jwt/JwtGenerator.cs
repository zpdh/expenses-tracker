using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ExpensesTracker.Domain.Entities;
using ExpensesTracker.Domain.Infrastructure.Tokens;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ExpensesTracker.Infrastructure.Authentication.Jwt;

public sealed class JwtGenerator : IJwtGenerator
{
    private readonly JwtOptions _options;

    public JwtGenerator(IOptions<JwtOptions> options)
    {
        _options = options.Value;
    }

    public string Generate(User user)
    {
        var claims = GenerateClaims(user);

        var signingCredentials = GenerateSigningCredentials();

        var tokenAsString = GenerateToken(claims, signingCredentials);

        return tokenAsString;
    }

    private static Claim[] GenerateClaims(User user)
    {
        return
        [
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email)
        ];
    }

    private SigningCredentials GenerateSigningCredentials()
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey));

        return new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
    }

    private string GenerateToken(Claim[] claims, SigningCredentials signingCredentials)
    {
        var token = new JwtSecurityToken(_options.Issuer, _options.Audience, claims, null, DateTime.UtcNow.AddHours(1), signingCredentials);

        var tokenHandler = new JwtSecurityTokenHandler();

        var tokenValue = tokenHandler.WriteToken(token);
        return tokenValue;
    }

}
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Application.Abstractions.Authentication;
using Domain.Users;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using SharedKernel.Time;

namespace Infrastructure.Authentication;

internal sealed class TokenProvider(IConfiguration configuration, IDateTimeProvider timeProvider)
    : ITokenProvider
{
    private const int RefreshTokenSize = 32;

    public string Create(User user)
    {
        string secretKey = configuration["Jwt:Secret"]!;

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var roleName = user.Roles.Select(r => r.Name).ToList();

        var permissions = user.Roles.SelectMany(r => r.Permissions).Select(p => p.Name).ToHashSet();

        List<Claim> claims =
        [
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new(JwtRegisteredClaimNames.Email, user.Email),
        ];

        claims.AddRange(roleName.Select(r => new Claim(ClaimTypes.Role, r)));
        claims.AddRange(permissions.Select(p => new Claim("permission", p)));

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = timeProvider.UtcNow.AddMinutes(
                configuration.GetValue<int>("Jwt:ExpirationInMinutes")
            ),
            SigningCredentials = credentials,
            Issuer = configuration["Jwt:Issuer"],
            Audience = configuration["Jwt:Audience"],
        };

        var handler = new JsonWebTokenHandler();

        string token = handler.CreateToken(tokenDescriptor);

        return token;
    }

    public string GenerateRefreshToken()
    {
        return Convert.ToBase64String(RandomNumberGenerator.GetBytes(RefreshTokenSize));
    }
}

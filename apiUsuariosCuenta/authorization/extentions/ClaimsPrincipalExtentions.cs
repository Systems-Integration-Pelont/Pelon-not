using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace apiUsuariosCuenta.authorization.extentions;

public static class ClaimsPrincipalExtentions
{
  public static Guid GetUserId(string? jwt)
  {
    JwtSecurityToken token = new JwtSecurityToken(jwt ?? string.Empty);
    string? userId = token.Subject;

    return Guid.TryParse(userId, out Guid parsedUserId)
      ? parsedUserId
      : throw new ApplicationException("User id is unavailable");
  }

  public static HashSet<string> GetPermissions(string? jwt)
  {
    JwtSecurityToken token = new JwtSecurityToken(jwt ?? string.Empty);

    HashSet<string> permissions =
      token?.Claims.Where(c => c.Type == "permission").Select(c => c.Value).ToHashSet()
      ?? [];
    
    return permissions;
  }
}
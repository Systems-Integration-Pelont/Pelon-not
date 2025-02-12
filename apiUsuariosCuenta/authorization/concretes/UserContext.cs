using apiUsuariosCuenta.authorization.extentions;
using apiUsuariosCuenta.Authorization.interfaces;

namespace apiUsuariosCuenta.authorization.concretes;

public class UserContext (IHttpContextAccessor httpContextAccessor) : IUserContext
{
  public Guid UserId => ClaimsPrincipalExtentions.GetUserId(this.Jwt);
  
  public string? Jwt =>
    httpContextAccessor.HttpContext?.Request.GetJwtToken();
  
  public HashSet<string> Permissions => ClaimsPrincipalExtentions.GetPermissions(this.Jwt);
}
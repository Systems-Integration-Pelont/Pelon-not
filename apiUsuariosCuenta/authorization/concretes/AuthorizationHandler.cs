using apiUsuariosCuenta.Authorization.interfaces;

namespace apiUsuariosCuenta.authorization.concretes;

public class AuthorizationHandler (IUserContext userContext) : IAuthorizationHandler
{
  public async Task<bool> HandleAsync(
    Permitions permissionAccess,
    CancellationToken cancellationToken = default
  )
  {
    HashSet<string> permissions = userContext.Permissions;

    return permissions.Contains(permissionAccess.Key);
  }

  public async Task<bool> HandleAsync(
    Guid ownerId,
    Permitions permissionAccess,
    CancellationToken cancellationToken = default
  )
  {
    bool handle = await HandleAsync(permissionAccess, cancellationToken);

    if (handle)
    {
      return true;
    }

    HashSet<string> permissions = userContext.Permissions;

    return permissions.Contains(permissionAccess.SelfAccessKey) && ownerId == userContext.UserId;
  }
}
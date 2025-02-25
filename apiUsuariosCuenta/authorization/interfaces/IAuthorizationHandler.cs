using apiUsuariosCuenta.authorization.concretes;

namespace apiUsuariosCuenta.Authorization.interfaces;

public interface IAuthorizationHandler
{
  Task<bool> HandleAsync(
    Permitions permitions,
    CancellationToken cancellationToken = default
  );

  Task<bool> HandleAsync(
    Guid ownerId,
    Permitions permitions,
    CancellationToken cancellationToken = default
  );
}
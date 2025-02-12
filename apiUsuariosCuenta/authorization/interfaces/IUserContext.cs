namespace apiUsuariosCuenta.Authorization.interfaces;

public interface IUserContext
{
  Guid UserId { get; }

  string? Jwt { get; }

  HashSet<string> Permissions { get; }
}
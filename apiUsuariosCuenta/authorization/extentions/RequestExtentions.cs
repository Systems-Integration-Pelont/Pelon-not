namespace apiUsuariosCuenta.authorization.extentions;

internal static class RequestExtensions
{
  public static string? GetJwtToken(this HttpRequest request)
  {
    string? bearerToken = request.Headers.Authorization;

    if (string.IsNullOrWhiteSpace(bearerToken))
    { 
      return null;
    }

    return bearerToken.StartsWith("Bearer ", StringComparison.Ordinal)
      ? bearerToken["Bearer ".Length..]
      : null;
  }
}
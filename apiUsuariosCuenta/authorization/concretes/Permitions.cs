namespace apiUsuariosCuenta.authorization.concretes;

public class Permitions(string resource, string operation)
{
  private string Resource { get; init; } = resource;
  private string Operation { get; init; } = operation;

  public string Key => $"{Resource}:{Operation}";
  public string SelfAccessKey => $"{Resource}:{Operation}:self";
}
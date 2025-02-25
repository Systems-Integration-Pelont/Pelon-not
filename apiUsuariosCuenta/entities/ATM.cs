namespace apiUsuariosCuenta.entities;

public class ATM
{
  public Guid ATMID { get; set; }
  public double? Funds { get; set; }
  public string? Direction { get; set; }
}
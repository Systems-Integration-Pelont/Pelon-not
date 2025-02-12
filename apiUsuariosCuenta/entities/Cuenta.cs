using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace apiUsuariosCuenta.entities;

public class Cuenta
{
  [Key]
  public Guid AccountId { get; set; }
  public required Guid UserId { get; set; }
  [ForeignKey(nameof(UserId))]
  public Usuario? usuario { get; set; }
  public int? Code { get; set; }
  public double? Amount { get; set; }
}
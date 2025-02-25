namespace apiUsuariosCuenta.entities;
using System.ComponentModel.DataAnnotations;

public class Rol
{
  [Key]
  public Guid RolId { get; set; }
  public string Name { get; set; }
  public int? AccessLevel { get; set; }
}
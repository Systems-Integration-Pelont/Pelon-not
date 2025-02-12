using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace apiUsuariosCuenta.entities;

public class UserAccess
{
  [Key]
  public Guid UserAccessId { get; set; }
  public required Guid? RolID { get; set; }
  [ForeignKey(nameof(RolID))]
  public Rol? rol { get; set; }
  
  public required Guid? UserID { get; set; }
  [ForeignKey(nameof(UserID))]
  public Usuario? user { get; set; }
  public string? UserName { get; set; }
  public string? Password { get; set; }
}
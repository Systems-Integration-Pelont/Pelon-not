using System.ComponentModel.DataAnnotations;

namespace apiUsuariosCuenta.entities;

public class Usuario
{
  
  public static readonly Guid User = Guid.Parse("0194d7da-5670-7be6-a484-a5742480aa94");
  public static readonly Guid Admin = Guid.Parse("0194d7da-4602-7c17-8676-045858fa6199");
  
  [Key]
  public Guid UserId { get; set; }
  public string Name { get; set; }
}

using System.ComponentModel.DataAnnotations;

namespace apiUsuariosCuenta.entities;

public class SelfOperationType
{
  [Key]
  public Guid SelfOperationTypeId { get; set; }
  public string TypeName {get; set;}
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace apiUsuariosCuenta.entities;

public class InterUserTransactionType
{
  [Key]
  public Guid InterUserTransactionTypeID { get; set; }
  public string TypeName {get; set;}
}
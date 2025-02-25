using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace apiUsuariosCuenta.entities;

public class InterUserTransaction
{
  [Key]
  public Guid InterUserTransactionID { get; set; }
  public required Guid? ToBankAccountID { get; set; }
  [ForeignKey(nameof(ToBankAccountID))]
  public required Guid? FromBankAccountID { get; set; }
  [ForeignKey(nameof(FromBankAccountID))]
  public required Guid? InterUserTransactionTypeID { get; set; }
  [ForeignKey(nameof(InterUserTransactionTypeID))]
  public InterUserTransactionType? Type { get; set; }
  public Cuenta? Cuenta { get; set; }
  public double? Amount { get; set; }
  public DateTime? DateTime { get; set; }
}
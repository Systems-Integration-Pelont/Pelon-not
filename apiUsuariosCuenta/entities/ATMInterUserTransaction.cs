using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace apiUsuariosCuenta.entities;

public class ATMInterUserTransaction
{
  [Key]
  public Guid ATMInterUserTransactionID { get; set; }
  public required Guid? ToBankAccountID { get; set; }
  [ForeignKey(nameof(ToBankAccountID))]
  public required Guid? FromBankAccountID { get; set; }
  [ForeignKey(nameof(FromBankAccountID))]
  public required Guid? ATMID { get; set; }
  [ForeignKey(nameof(ATMID))]
  public required Guid? InterUserTransactionTypeID { get; set; }
  [ForeignKey(nameof(InterUserTransactionTypeID))]
  public InterUserTransactionType? Type { get; set; }
  public Cuenta? Cuenta { get; set; }
  public ATM? Atm { get; set; }
  public double? Amount { get; set; }
  public DateTime? DateTime { get; set; }
}
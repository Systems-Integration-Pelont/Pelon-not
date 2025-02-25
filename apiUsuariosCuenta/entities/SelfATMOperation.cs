using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace apiUsuariosCuenta.entities;

public class SelfATMOperation
{
  [Key]
  public Guid SelfATMOperationID { get; set; }
  public required Guid? BankAcountID { get; set; }
  [ForeignKey(nameof(BankAcountID))]
  public required Guid? ATMID { get; set; }
  [ForeignKey(nameof(ATMID))]
  public required Guid? SelfOperationTypeId { get; set; }
  [ForeignKey(nameof(SelfOperationTypeId))]
  public SelfOperationType? Type { get; set; }
  public Cuenta? Cuenta { get; set; }
  public ATM? Atm { get; set; }
  public double? Amount { get; set; }
  public DateTime? DateTime { get; set; }
}
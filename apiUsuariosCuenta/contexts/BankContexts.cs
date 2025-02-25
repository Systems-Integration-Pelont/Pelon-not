using apiUsuariosCuenta.entities;
using Microsoft.EntityFrameworkCore;

namespace apiUsuariosCuenta.contexts;

public class BankContexts (DbContextOptions banco) : DbContext (banco)
{
  public DbSet<Usuario> Usuarios { get; set; }
  public DbSet<Cuenta> Cuentas { get; set; }
  public DbSet<Rol> Rols { get; set; }
  public DbSet<UserAccess> UserAccesses { get; set; }
  public DbSet<SelfOperation> SelfOperations { get; set; }
  public DbSet<SelfATMOperation> SelfAtmOperations { get; set; }
  public DbSet<InterUserTransaction> InterUserTransactions { get; set; }
  public DbSet<ATM> Atms { get; set; }
  public DbSet<ATMInterUserTransaction> ATMInterUserTransactions { get; set; }
  
  public DbSet<SelfOperationType> SelfOperationTypes { get; set; }
  public DbSet<InterUserTransactionType> InterUserTransactionTypes { get; set; }
}
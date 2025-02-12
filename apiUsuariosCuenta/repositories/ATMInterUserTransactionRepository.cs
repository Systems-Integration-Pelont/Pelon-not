using apiUsuariosCuenta.contexts;
using apiUsuariosCuenta.entities;
using Microsoft.EntityFrameworkCore;

namespace apiUsuariosCuenta.repositories;

public class ATMInterUserTransactionRepository : IRepository<ATMInterUserTransaction>
{
  private readonly BankContexts _context;

  public ATMInterUserTransactionRepository(BankContexts context)
  {
    _context = context;
  }

  public async Task<IEnumerable<ATMInterUserTransaction>> GetAll()
  {
    return await _context.ATMInterUserTransactions.ToListAsync();
  }

  public async Task<ATMInterUserTransaction?> GetById(Guid id)
  {
    return await _context.ATMInterUserTransactions.FindAsync(id);
  }

  public async Task<bool> Delete(Guid id)
  {
    var u = await _context.ATMInterUserTransactions.FindAsync(id);
    if (u != null)
    {
      _context.ATMInterUserTransactions.Remove(u);
      await _context.SaveChangesAsync();
      return true;
    }
    return false;
  }

  public async Task<ATMInterUserTransaction> Add(ATMInterUserTransaction entity)
  {
    var destiny = await _context.Cuentas.FindAsync(entity.ToBankAccountID);
    var sender = await _context.Cuentas.FindAsync(entity.FromBankAccountID);
    var atm = await _context.Atms.FindAsync(entity.ATMID);

    if (destiny != null && sender != null && atm != null)
    {
      destiny.Amount += entity.Amount;
      sender.Amount -= entity.Amount;
      atm.Funds -= entity.Amount;
      _context.Cuentas.Update(destiny);
      _context.Cuentas.Update(sender);
      _context.Atms.Update(atm);
      await _context.ATMInterUserTransactions.AddAsync(entity);
      await _context.SaveChangesAsync();
    }
    return entity;
  }
  public async Task<ATMInterUserTransaction> Update(ATMInterUserTransaction entity)
  {
    _context.ATMInterUserTransactions.Update(entity);
    await _context.SaveChangesAsync();
    return entity;
  }

  public async Task<IEnumerable<ATMInterUserTransaction>> GetByUserId(Guid userId)
  {
    var userAccounts = _context.Cuentas.Where(c => c.UserId.Equals(userId)).ToList();

    if (!userAccounts.Any())
      return Enumerable.Empty<ATMInterUserTransaction>();

    var userAccountIds = userAccounts.Select(a => a.AccountId).ToList();

    var transactions = await _context.ATMInterUserTransactions
      .Where(t => userAccountIds.Contains(t.FromBankAccountID ?? Guid.NewGuid()) || 
                  userAccountIds.Contains(t.ToBankAccountID ?? Guid.NewGuid()))
      .ToListAsync();
    
    return transactions;
  }
}
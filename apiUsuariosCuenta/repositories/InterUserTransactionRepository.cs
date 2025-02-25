using apiUsuariosCuenta.contexts;
using apiUsuariosCuenta.entities;
using Microsoft.EntityFrameworkCore;

namespace apiUsuariosCuenta.repositories;

public class InterUserTransactionRepository : IRepository<InterUserTransaction>
{
  private readonly BankContexts _context;

  public InterUserTransactionRepository(BankContexts context)
  {
    _context = context;
  }

  public async Task<IEnumerable<InterUserTransaction>> GetAll()
  {
    return await _context.InterUserTransactions.ToListAsync();
  }

  public async Task<InterUserTransaction?> GetById(Guid id)
  {
    return await _context.InterUserTransactions.FindAsync(id);
  }

  public async Task<bool> Delete(Guid id)
  {
    var u = await _context.InterUserTransactions.FindAsync(id);
    if (u != null)
    {
      _context.InterUserTransactions.Remove(u);
      await _context.SaveChangesAsync();
      return true;
    }
    return false;
  }

  public async Task<InterUserTransaction> Add(InterUserTransaction entity)
  {
    var destiny = await _context.Cuentas.FindAsync(entity.ToBankAccountID);
    var sender = await _context.Cuentas.FindAsync(entity.FromBankAccountID);
    
    if (destiny != null && sender != null)
    {
      destiny.Amount += entity.Amount;
      sender.Amount -= entity.Amount;
      _context.Cuentas.Update(destiny);
      _context.Cuentas.Update(sender);
      await _context.InterUserTransactions.AddAsync(entity);
      await _context.SaveChangesAsync();
    }
    
    return entity;
  }

  public async Task<InterUserTransaction> Update(InterUserTransaction entity)
  {
    _context.InterUserTransactions.Update(entity);
    await _context.SaveChangesAsync();
    return entity;
  }

  public async Task<IEnumerable<InterUserTransaction>> GetByUserId(Guid userId)
  {
    var userAccounts = _context.Cuentas.Where(c => c.UserId.Equals(userId)).ToList();

    if (!userAccounts.Any())
      return Enumerable.Empty<InterUserTransaction>();

    var userAccountIds = userAccounts.Select(a => a.AccountId).ToList();

    var transactions = await _context.InterUserTransactions
      .Where(t => userAccountIds.Contains(t.FromBankAccountID ?? Guid.Empty) || 
                  userAccountIds.Contains(t.ToBankAccountID ?? Guid.Empty))
      .ToListAsync();
    
    return transactions;
  }
}
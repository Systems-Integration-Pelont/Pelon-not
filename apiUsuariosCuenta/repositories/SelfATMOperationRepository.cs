using apiUsuariosCuenta.contexts;
using apiUsuariosCuenta.entities;
using Microsoft.EntityFrameworkCore;

namespace apiUsuariosCuenta.repositories;

public class SelfATMOperationRepository : IRepository<SelfATMOperation>
{
  private readonly BankContexts _context;

  public SelfATMOperationRepository(BankContexts context)
  {
    _context = context;
  }

  public async Task<IEnumerable<SelfATMOperation>> GetAll()
  {
    return await _context.SelfAtmOperations.ToListAsync();
  }

  public async Task<SelfATMOperation?> GetById(Guid id)
  {
    return await _context.SelfAtmOperations.FindAsync(id);
  }

  public async Task<bool> Delete(Guid id)
  {
    var u = await _context.SelfAtmOperations.FindAsync(id);
    if (u != null)
    {
      _context.SelfAtmOperations.Remove(u);
      await _context.SaveChangesAsync();
      return true;
    }
    return false;
  }

  public async Task<SelfATMOperation> Add(SelfATMOperation entity)
  {
    var account = await _context.Cuentas.FindAsync(entity.BankAcountID);
    var atm = await _context.Atms.FindAsync(entity.ATMID);

    if (account != null && atm != null)
    {
      account.Amount += entity.Amount;
      atm.Funds -= entity.Amount;
      _context.Cuentas.Update(account);
      _context.Atms.Update(atm);
      await _context.SelfAtmOperations.AddAsync(entity);
      await _context.SaveChangesAsync();
    }
    return entity;
  }

  public async Task<SelfATMOperation> Update(SelfATMOperation entity)
  {
    _context.SelfAtmOperations.Update(entity);
    await _context.SaveChangesAsync();
    return entity;
  }

  public async Task<IEnumerable<SelfATMOperation>> GetByUserId(Guid userId)
  {
    var userAccounts = _context.Cuentas.Where(c => c.UserId.Equals(userId)).ToList();

    if (!userAccounts.Any())
      return Enumerable.Empty<SelfATMOperation>();

    var userAccountIds = userAccounts.Select(a => a.AccountId).ToList();

    var transactions = await _context.SelfAtmOperations
      .Where(t => userAccountIds.Contains(t.BankAcountID ?? Guid.Empty))
      .ToListAsync();
    
    return transactions;
  }
}
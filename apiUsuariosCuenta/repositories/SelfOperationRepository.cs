using apiUsuariosCuenta.contexts;
using apiUsuariosCuenta.entities;
using Microsoft.EntityFrameworkCore;

namespace apiUsuariosCuenta.repositories;

public class SelfOperationRepository : IRepository<SelfOperation>
{
  private readonly BankContexts _context;

  public SelfOperationRepository(BankContexts context)
  {
    _context = context;
  }

  public async Task<IEnumerable<SelfOperation>> GetAll()
  {
    return await _context.SelfOperations.ToListAsync();
  }

  public async Task<SelfOperation?> GetById(Guid id)
  {
    return await _context.SelfOperations.FindAsync(id);
  }

  public async Task<bool> Delete(Guid id)
  {
    var u = await _context.SelfOperations.FindAsync(id);
    if (u != null)
    {
      _context.SelfOperations.Remove(u);
      await _context.SaveChangesAsync();
      return true;
    }
    return false;
  }

  public async Task<SelfOperation> Add(SelfOperation entity)
  {
    var account = await _context.Cuentas.FindAsync(entity.BankAcountID);
    if (account != null)
    {
      account.Amount += entity.Amount;
      _context.Cuentas.Update(account);
      await _context.SelfOperations.AddAsync(entity);
      await _context.SaveChangesAsync();
    }
    return entity;
  }

  public async Task<SelfOperation> Update(SelfOperation entity)
  {
    _context.SelfOperations.Update(entity);
    await _context.SaveChangesAsync();
    return entity;
  }

  public async Task<IEnumerable<SelfOperation>> GetByUserId(Guid userId)
  {
    var userAccounts = _context.Cuentas.Where(c => c.UserId.Equals(userId)).ToList();

    if (!userAccounts.Any())
      return Enumerable.Empty<SelfOperation>();

    var userAccountIds = userAccounts.Select(a => a.AccountId).ToList();

    var transactions = await _context.SelfOperations
      .Where(t => userAccountIds.Contains(t.BankAcountID ?? Guid.Empty))
      .ToListAsync();
    
    return transactions;
  }
}
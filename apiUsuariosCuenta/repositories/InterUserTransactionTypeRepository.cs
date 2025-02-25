using apiUsuariosCuenta.contexts;
using apiUsuariosCuenta.entities;
using Microsoft.EntityFrameworkCore;

namespace apiUsuariosCuenta.repositories;


public class InterUserTransactionTypeRepository: IRepository<InterUserTransactionType>
{
  private readonly BankContexts _context;

  public InterUserTransactionTypeRepository(BankContexts context)
  {
    _context = context;
  }

  public async Task<IEnumerable<InterUserTransactionType>> GetAll()
  {
    return await _context.InterUserTransactionTypes.ToListAsync();
  }

  public async Task<InterUserTransactionType?> GetById(Guid id)
  {
    return await _context.InterUserTransactionTypes.FindAsync(id);
  }

  public async Task<bool> Delete(Guid id)
  {
    var u = await _context.InterUserTransactionTypes.FindAsync(id);
    if (u != null)
    {
      _context.InterUserTransactionTypes.Remove(u);
      await _context.SaveChangesAsync();
      return true;
    }
    return false;
  }

  public async Task<InterUserTransactionType> Add(InterUserTransactionType entity)
  {
    await _context.InterUserTransactionTypes.AddAsync(entity);
    await _context.SaveChangesAsync();
    return entity;
  }

  public async Task<InterUserTransactionType> Update(InterUserTransactionType entity)
  {
    _context.InterUserTransactionTypes.Update(entity);
    await _context.SaveChangesAsync();
    return entity;
  }
}
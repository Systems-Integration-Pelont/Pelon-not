using apiUsuariosCuenta.contexts;
using apiUsuariosCuenta.entities;
using Microsoft.EntityFrameworkCore;

namespace apiUsuariosCuenta.repositories;

public class AtmRepository: IRepository<ATM>
{
  private readonly BankContexts _context;

  public AtmRepository(BankContexts context)
  {
    _context = context;
  }

  public async Task<IEnumerable<ATM>> GetAll()
  {
    return await _context.Atms.ToListAsync();
  }

  public async Task<ATM?> GetById(Guid id)
  {
    return await _context.Atms.FindAsync(id);
  }

  public async Task<bool> Delete(Guid id)
  {
    var u = await _context.Atms.FindAsync(id);
    if (u != null)
    {
      _context.Atms.Remove(u);
      await _context.SaveChangesAsync();
      return true;
    }
    return false;
  }

  public async Task<ATM> Add(ATM entity)
  {
    await _context.Atms.AddAsync(entity);
    await _context.SaveChangesAsync();
    return entity;
  }

  public async Task<ATM> Update(ATM entity)
  {
    _context.Atms.Update(entity);
    await _context.SaveChangesAsync();
    return entity;
  }
}
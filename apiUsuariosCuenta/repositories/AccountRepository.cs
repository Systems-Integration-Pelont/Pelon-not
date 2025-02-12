using apiUsuariosCuenta.contexts;
using apiUsuariosCuenta.entities;
using Microsoft.EntityFrameworkCore;

namespace apiUsuariosCuenta.repositories;


public class AccountRepository: IRepository<Cuenta>
{
  private readonly BankContexts _context;

  public AccountRepository(BankContexts context)
  {
    _context = context;
  }

  public async Task<IEnumerable<Cuenta>> GetAll()
  {
    return await _context.Cuentas.ToListAsync();
  }

  public async Task<Cuenta?> GetById(Guid id)
  {
    return await _context.Cuentas.FindAsync(id);
  }

  public async Task<bool> Delete(Guid id)
  {
    var u = await _context.Cuentas.FindAsync(id);
    if (u != null)
    {
      _context.Cuentas.Remove(u);
      await _context.SaveChangesAsync();
      return true;
    }
    return false;
  }

  public async Task<Cuenta> Add(Cuenta entity)
  {
    await _context.Cuentas.AddAsync(entity);
    await _context.SaveChangesAsync();
    return entity;
  }

  public async Task<Cuenta> Update(Cuenta entity)
  {
    _context.Cuentas.Update(entity);
    await _context.SaveChangesAsync();
    return entity;
  }
}
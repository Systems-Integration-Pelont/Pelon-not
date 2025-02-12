using apiUsuariosCuenta.contexts;
using apiUsuariosCuenta.entities;
using Microsoft.EntityFrameworkCore;

namespace apiUsuariosCuenta.repositories;

public class RolRepository: IRepository<Rol>
{
  private readonly BankContexts _context;

  public RolRepository(BankContexts context)
  {
    _context = context;
  }

  public async Task<IEnumerable<Rol>> GetAll()
  {
    return await _context.Rols.ToListAsync();
  }

  public async Task<Rol?> GetById(Guid id)
  {
    return await _context.Rols.FindAsync(id);
  }

  public async Task<bool> Delete(Guid id)
  {
    var u = await _context.Rols.FindAsync(id);
    if (u != null)
    {
      _context.Rols.Remove(u);
      await _context.SaveChangesAsync();
      return true;
    }
    return false;
  }

  public async Task<Rol> Add(Rol entity)
  {
    await _context.Rols.AddAsync(entity);
    await _context.SaveChangesAsync();
    return entity;
  }

  public async Task<Rol> Update(Rol entity)
  {
    _context.Rols.Update(entity);
    await _context.SaveChangesAsync();
    return entity;
  }
}
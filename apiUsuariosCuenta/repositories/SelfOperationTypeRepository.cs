using apiUsuariosCuenta.contexts;
using apiUsuariosCuenta.entities;
using Microsoft.EntityFrameworkCore;

namespace apiUsuariosCuenta.repositories;


public class SelfOperationTypeRepository: IRepository<SelfOperationType>
{
  private readonly BankContexts _context;

  public SelfOperationTypeRepository(BankContexts context)
  {
    _context = context;
  }

  public async Task<IEnumerable<SelfOperationType>> GetAll()
  {
    return await _context.SelfOperationTypes.ToListAsync();
  }

  public async Task<SelfOperationType?> GetById(Guid id)
  {
    return await _context.SelfOperationTypes.FindAsync(id);
  }

  public async Task<bool> Delete(Guid id)
  {
    var u = await _context.SelfOperationTypes.FindAsync(id);
    if (u != null)
    {
      _context.SelfOperationTypes.Remove(u);
      await _context.SaveChangesAsync();
      return true;
    }
    return false;
  }

  public async Task<SelfOperationType> Add(SelfOperationType entity)
  {
    await _context.SelfOperationTypes.AddAsync(entity);
    await _context.SaveChangesAsync();
    return entity;
  }

  public async Task<SelfOperationType> Update(SelfOperationType entity)
  {
    _context.SelfOperationTypes.Update(entity);
    await _context.SaveChangesAsync();
    return entity;
  }
}
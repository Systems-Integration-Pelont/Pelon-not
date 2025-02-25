using apiUsuariosCuenta.contexts;
using apiUsuariosCuenta.entities;
using apiUsuariosCuenta.services;
using Microsoft.EntityFrameworkCore;

namespace apiUsuariosCuenta.repositories;

public class UserAccessRepository : IRepository<UserAccess>
{
  private readonly BankContexts _context;
  private readonly EncryptService _encryptService;
  public UserAccessRepository(BankContexts context, EncryptService encryptService)
  {
    _context = context;
    _encryptService = encryptService;
  }

  public async Task<IEnumerable<UserAccess>> GetAll()
  {
    return await _context.UserAccesses.ToListAsync();
  }

  public async Task<UserAccess?> GetById(Guid id)
  {
    return await _context.UserAccesses.FindAsync(id);
  }

  public async Task<bool> Delete(Guid id)
  {
    var u = await _context.UserAccesses.FindAsync(id);
    if (u != null)
    {
      _context.UserAccesses.Remove(u);
      await _context.SaveChangesAsync();
      return true;
    }
    return false;
  }

  public async Task<UserAccess> Add(UserAccess entity)
  {
    entity.Password = _encryptService.Encrypt(entity.Password);
    await _context.UserAccesses.AddAsync(entity);
    await _context.SaveChangesAsync();
    return entity;
  }

  public async Task<UserAccess> Update(UserAccess entity)
  {
    _context.UserAccesses.Update(entity);
    await _context.SaveChangesAsync();
    return entity;
  }
  
  
}
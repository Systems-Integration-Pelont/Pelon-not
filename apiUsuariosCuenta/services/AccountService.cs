using apiUsuariosCuenta.entities;
using apiUsuariosCuenta.repositories;
using apiUsuariosCuenta.services.Interfaces;

namespace apiUsuariosCuenta.services;

public class AccountService: IAccountService
{
  private readonly AccountRepository _repository;

  public AccountService(AccountRepository repository)
  {
    _repository = repository;
  }
  
  public async Task<IEnumerable<Cuenta>> GetAll()
  {
    return await _repository.GetAll();  }

  public async Task<Cuenta?> GetById(Guid id)
  {
    return await _repository.GetById(id);
  }

  public async Task<bool> Delete(Guid id)
  {
    return await _repository.Delete(id);
  }

  public async Task<Cuenta?> Add(Cuenta entity)
  {
    return await _repository.Add(entity);
  }

  public Task<Cuenta> Put(Cuenta entity)
  {
    return _repository.Update(entity);
  }

  public async Task<Cuenta> Patch(Guid id, Cuenta partialEntity)
  {
    var entity = await _repository.GetById(id);
    if (entity == null) return null;

    if (!partialEntity.Code.HasValue)
      entity.Code = partialEntity.Code;
    
    if (!partialEntity.Amount.HasValue)
      entity.Amount = partialEntity.Amount;
    
    await _repository.Update(entity);
    return partialEntity;
  }
}
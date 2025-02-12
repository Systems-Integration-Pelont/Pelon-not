using apiUsuariosCuenta.entities;
using apiUsuariosCuenta.repositories;
using apiUsuariosCuenta.services.Interfaces;

namespace apiUsuariosCuenta.services;

public class AtmService: IAtmService
{
  private readonly AtmRepository _repository;

  public AtmService(AtmRepository repository)
  {
    _repository = repository;
  }
  
  public async Task<IEnumerable<ATM>> GetAll()
  {
    return await _repository.GetAll();  }

  public async Task<ATM?> GetById(Guid id)
  {
    return await _repository.GetById(id);
  }

  public async Task<bool> Delete(Guid id)
  {
    return await _repository.Delete(id);
  }

  public async Task<ATM?> Add(ATM entity)
  {
    return await _repository.Add(entity);
  }

  public Task<ATM> Put(ATM entity)
  {
    return _repository.Update(entity);
  }

  public async Task<ATM> Patch(Guid id, ATM partialEntity)
  {
    var entity = await _repository.GetById(id);
    if (entity == null) return null;

    if (!partialEntity.Funds.HasValue)
      entity.Funds = partialEntity.Funds;
    
    if (string.IsNullOrEmpty(partialEntity.Direction))
      entity.Direction = partialEntity.Direction;
    
    await _repository.Update(entity);
    return partialEntity;
  }
}
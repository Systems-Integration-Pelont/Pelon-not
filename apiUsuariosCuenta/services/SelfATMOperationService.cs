using apiUsuariosCuenta.entities;
using apiUsuariosCuenta.repositories;
using apiUsuariosCuenta.services.Interfaces;

namespace apiUsuariosCuenta.services;

public class SelfATMOperationService : ISelfATMOperationService
{
  private readonly SelfATMOperationRepository _repository;

  public SelfATMOperationService(SelfATMOperationRepository repository)
  {
    _repository = repository;
  }
  
  public async Task<IEnumerable<SelfATMOperation>> GetAll()
  {
    return await _repository.GetAll();
  }

  public async Task<SelfATMOperation?> GetById(Guid id)
  {
    return await _repository.GetById(id);
  }

  public async Task<bool> Delete(Guid id)
  {
    return await _repository.Delete(id);
  }

  public async Task<SelfATMOperation?> Add(SelfATMOperation entity)
  {
    return await _repository.Add(entity);
  }

  public async Task<SelfATMOperation> Put(SelfATMOperation entity)
  {
    return await _repository.Update(entity);
  }

  public async Task<SelfATMOperation> Patch(Guid id, SelfATMOperation entity)
  {
    var partialEntity = await _repository.GetById(id);

    if (partialEntity == null) return null;

    if (!partialEntity.Amount.HasValue)
      partialEntity.Amount = entity.Amount;
    
    if (!partialEntity.SelfOperationTypeId.HasValue)
      partialEntity.SelfOperationTypeId = entity.SelfOperationTypeId;

    if(!partialEntity.DateTime.HasValue)
      partialEntity.DateTime = entity.DateTime;
    
    if (!partialEntity.ATMID.HasValue)
      partialEntity.ATMID = entity.ATMID;
    
    return partialEntity;
  }

  public async Task<IEnumerable<SelfATMOperation>> GetByUserID(Guid userId)
  {
    return await _repository.GetByUserId(userId);
  }

}
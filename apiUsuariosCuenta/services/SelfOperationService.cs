using apiUsuariosCuenta.entities;
using apiUsuariosCuenta.repositories;
using apiUsuariosCuenta.services.Interfaces;

namespace apiUsuariosCuenta.services;

public class SelfOperationService : ISelfOperationService
{
  private readonly SelfOperationRepository _repository;

  public SelfOperationService(SelfOperationRepository repository)
  {
    _repository = repository;
  }
  
  public async Task<IEnumerable<SelfOperation>> GetAll()
  {
    return await _repository.GetAll();
  }

  public async Task<SelfOperation?> GetById(Guid id)
  {
    return await _repository.GetById(id);
  }

  public async Task<bool> Delete(Guid id)
  {
    return await _repository.Delete(id);
  }

  public async Task<SelfOperation?> Add(SelfOperation entity)
  {
    return await _repository.Add(entity);
  }

  public async Task<SelfOperation> Put(SelfOperation entity)
  {
    return await _repository.Update(entity);
  }

  public async Task<SelfOperation> Patch(Guid id, SelfOperation entity)
  {
    var partialEntity = await _repository.GetById(id);

    if (partialEntity == null) return null;

    if (!string.IsNullOrEmpty(partialEntity.Description))
      partialEntity.Description= entity.Description;

    if (!partialEntity.Amount.HasValue)
      partialEntity.Amount = entity.Amount;
    
    if (!partialEntity.SelfOperationTypeId.HasValue)
      partialEntity.SelfOperationTypeId = entity.SelfOperationTypeId;

    if(!partialEntity.DateTime.HasValue)
      partialEntity.DateTime = entity.DateTime;
    
    return partialEntity;
  }

  public async Task<IEnumerable<SelfOperation>> GetByUserID(Guid userId)
  {
    return await _repository.GetByUserId(userId);
  }
}
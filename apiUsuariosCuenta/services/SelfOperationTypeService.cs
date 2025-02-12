using apiUsuariosCuenta.entities;
using apiUsuariosCuenta.repositories;
using apiUsuariosCuenta.services.Interfaces;

namespace apiUsuariosCuenta.services;

public class SelfOperationTypeService : ISelfOperationTypeService
{
  private readonly SelfOperationTypeRepository _repository;

  public SelfOperationTypeService(SelfOperationTypeRepository repository)
  {
    _repository = repository;
  }


  public async Task<IEnumerable<SelfOperationType>> GetAll()
  {
    return await _repository.GetAll();
  }

  public async Task<SelfOperationType?> GetById(Guid id)
  {
    return await _repository.GetById(id);
  }

  public async Task<bool> Delete(Guid id)
  {
    return await _repository.Delete(id);
  }

  public async Task<SelfOperationType?> Add(SelfOperationType entity)
  {
    return await _repository.Add(entity);
  }

  public async Task<SelfOperationType> Put(SelfOperationType entity)
  {
    return await _repository.Update(entity);
  }

  public async Task<SelfOperationType> Patch(Guid id, SelfOperationType entity)
  {
    var partialEntity = await _repository.GetById(id);

    if (partialEntity == null) return null;

    if (string.IsNullOrEmpty(partialEntity.TypeName))
      partialEntity.TypeName = entity.TypeName;

    return partialEntity;
  }
}
using apiUsuariosCuenta.entities;
using apiUsuariosCuenta.repositories;
using apiUsuariosCuenta.services.Interfaces;

namespace apiUsuariosCuenta.services;

public class InterUserTransactionTypeService : IInterUserTransactionTypeService
{
  private readonly InterUserTransactionTypeRepository _repository;

  public InterUserTransactionTypeService(InterUserTransactionTypeRepository repository)
  {
    _repository = repository;
  }


  public async Task<IEnumerable<InterUserTransactionType>> GetAll()
  {
    return await _repository.GetAll();
  }

  public async Task<InterUserTransactionType?> GetById(Guid id)
  {
    return await _repository.GetById(id);
  }

  public async Task<bool> Delete(Guid id)
  {
    return await _repository.Delete(id);
  }

  public async Task<InterUserTransactionType?> Add(InterUserTransactionType entity)
  {
    return await _repository.Add(entity);
  }

  public async Task<InterUserTransactionType> Put(InterUserTransactionType entity)
  {
    return await _repository.Update(entity);
  }

  public async Task<InterUserTransactionType> Patch(Guid id, InterUserTransactionType entity)
  {
    var partialEntity = await _repository.GetById(id);

    if (partialEntity == null) return null;

    if (string.IsNullOrEmpty(partialEntity.TypeName))
      partialEntity.TypeName = entity.TypeName;

    return partialEntity;
  }
}
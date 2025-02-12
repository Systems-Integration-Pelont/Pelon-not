using apiUsuariosCuenta.entities;
using apiUsuariosCuenta.repositories;
using apiUsuariosCuenta.services.Interfaces;

namespace apiUsuariosCuenta.services;

public class InterUserTransactionService : IInterUserTransactionService
{
  private readonly InterUserTransactionRepository _repository;

  public InterUserTransactionService(InterUserTransactionRepository repository)
  {
    _repository = repository;
  }
  
  public async Task<IEnumerable<InterUserTransaction>> GetAll()
  {
    return await _repository.GetAll();
  }

  public async Task<InterUserTransaction?> GetById(Guid id)
  {
    return await _repository.GetById(id);
  }

  public async Task<bool> Delete(Guid id)
  {
    return await _repository.Delete(id);
  }

  public async Task<InterUserTransaction?> Add(InterUserTransaction entity)
  {
    return await _repository.Add(entity);
  }

  public async Task<InterUserTransaction> Put(InterUserTransaction entity)
  {
    return await _repository.Update(entity);
  }

  public async Task<InterUserTransaction> Patch(Guid id, InterUserTransaction entity)
  {
    var partialEntity = await _repository.GetById(id);

    if (partialEntity == null) return null;

    if (!partialEntity.Amount.HasValue)
      partialEntity.Amount = entity.Amount;
    
    if (!partialEntity.ToBankAccountID.HasValue)
      partialEntity.ToBankAccountID = entity.ToBankAccountID;
    
    if (!partialEntity.FromBankAccountID.HasValue)
      partialEntity.FromBankAccountID = entity.FromBankAccountID;
    
    if(!partialEntity.DateTime.HasValue)
      partialEntity.DateTime = entity.DateTime;
    
    if (!partialEntity.InterUserTransactionTypeID.HasValue)
      partialEntity.InterUserTransactionTypeID = entity.InterUserTransactionTypeID;
    
    return partialEntity;
  }

  public async Task<IEnumerable<InterUserTransaction>> GetByUserID(Guid userId)
  {
    return await _repository.GetByUserId(userId);
  }

}
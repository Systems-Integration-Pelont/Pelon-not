using apiUsuariosCuenta.entities;
using apiUsuariosCuenta.repositories;
using apiUsuariosCuenta.services.Interfaces;

namespace apiUsuariosCuenta.services;

public class ATMInterUserTransactionService : IATMInterUserTransactionService
{
  private readonly ATMInterUserTransactionRepository _repository;

  public ATMInterUserTransactionService(ATMInterUserTransactionRepository repository)
  {
    _repository = repository;
  }
  
  public async Task<IEnumerable<ATMInterUserTransaction>> GetAll()
  {
    return await _repository.GetAll();
  }

  public async Task<ATMInterUserTransaction?> GetById(Guid id)
  {
    return await _repository.GetById(id);
  }

  public async Task<bool> Delete(Guid id)
  {
    return await _repository.Delete(id);
  }

  public async Task<ATMInterUserTransaction?> Add(ATMInterUserTransaction entity)
  {
    return await _repository.Add(entity);
  }

  public async Task<ATMInterUserTransaction> Put(ATMInterUserTransaction entity)
  {
    return await _repository.Update(entity);
  }

  public async Task<ATMInterUserTransaction> Patch(Guid id, ATMInterUserTransaction entity)
  {
    var partialEntity = await _repository.GetById(id);

    if (partialEntity == null) return null;

    if (!partialEntity.Amount.HasValue)
      partialEntity.Amount = entity.Amount;
    
    if (!partialEntity.ToBankAccountID.HasValue)
      partialEntity.ToBankAccountID = entity.ToBankAccountID;
    
    if (!partialEntity.ATMID.HasValue)
      partialEntity.ATMID = entity.ATMID;
    
    if (!partialEntity.FromBankAccountID.HasValue)
      partialEntity.FromBankAccountID = entity.FromBankAccountID;
    
    if(!partialEntity.DateTime.HasValue)
      partialEntity.DateTime = entity.DateTime;
    
    if (!partialEntity.InterUserTransactionTypeID.HasValue)
      partialEntity.InterUserTransactionTypeID = entity.InterUserTransactionTypeID;
    
    return partialEntity;
  }

  public async Task<IEnumerable<ATMInterUserTransaction>> GetByUserID(Guid userId)
  {
    return await _repository.GetByUserId(userId);
  }

}
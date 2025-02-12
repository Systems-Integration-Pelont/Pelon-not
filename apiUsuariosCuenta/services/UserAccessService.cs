using apiUsuariosCuenta.entities;
using apiUsuariosCuenta.repositories;
using apiUsuariosCuenta.services.Interfaces;

namespace apiUsuariosCuenta.services;

public class UserAccessService : IUserAccessService
{
  private readonly UserAccessRepository _repository;

  public UserAccessService(UserAccessRepository repository)
  {
    _repository = repository;
  }
  
  public async Task<IEnumerable<UserAccess>> GetAll()
  {
    return await _repository.GetAll();  }

  public async Task<UserAccess?> GetById(Guid id)
  {
    return await _repository.GetById(id);
  }

  public async Task<bool> Delete(Guid id)
  {
    return await _repository.Delete(id);
  }

  public async Task<UserAccess?> Add(UserAccess entity)
  {
    return await _repository.Add(entity);
  }

  public Task<UserAccess> Put(UserAccess entity)
  {
    return _repository.Update(entity);
  }

  public async Task<UserAccess> Patch(Guid id, UserAccess partialEntity)
  {
    var entity = await _repository.GetById(id);
    if (entity == null) return null;

    if (!partialEntity.RolID.HasValue)
      entity.RolID = partialEntity.RolID;
    
    if (string.IsNullOrEmpty(partialEntity.UserName))
      entity.UserName = partialEntity.UserName;
    
    if (string.IsNullOrEmpty(partialEntity.Password))
      entity.Password = partialEntity.Password;
    
    await _repository.Update(entity);
    return partialEntity;
  }
}
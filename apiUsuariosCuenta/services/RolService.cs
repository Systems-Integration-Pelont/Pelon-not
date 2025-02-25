using apiUsuariosCuenta.entities;
using apiUsuariosCuenta.repositories;
using apiUsuariosCuenta.services.Interfaces;

namespace apiUsuariosCuenta.services;

public class RolService : IRolService
{
  private readonly RolRepository _repository;

  public RolService(RolRepository repository)
  {
    _repository = repository;
  }


  public async Task<IEnumerable<Rol>> GetAll()
  {
    return await _repository.GetAll();
  }

  public async Task<Rol?> GetById(Guid id)
  {
    return await _repository.GetById(id);
  }

  public async Task<bool> Delete(Guid id)
  {
    return await _repository.Delete(id);
  }

  public async Task<Rol?> Add(Rol entity)
  {
    return await _repository.Add(entity);
  }

  public async Task<Rol> Put(Rol entity)
  {
    return await _repository.Update(entity);
  }

  public async Task<Rol> Patch(Guid id, Rol entity)
  {
    var partialEntity = await _repository.GetById(id);

    if (partialEntity == null) return null;

    if (string.IsNullOrEmpty(partialEntity.Name))
      partialEntity.Name = entity.Name;

    if (partialEntity.AccessLevel != null)
      partialEntity.AccessLevel = entity.AccessLevel;

    return partialEntity;
  }
}
using System.Collections;
using apiUsuariosCuenta.dtos;
using apiUsuariosCuenta.entities;
using apiUsuariosCuenta.repositories;
using apiUsuariosCuenta.services.Interfaces;

namespace apiUsuariosCuenta.services;

public class UserService: IUserService
{
  private readonly UserRepository _repository;

  public UserService(UserRepository repository)
  {
    _repository = repository;
  }
  
  public async Task<IEnumerable<Usuario>> GetAll()
  {
    return await _repository.GetAll();  }

  public async Task<Usuario?> GetById(Guid id)
  {
    return await _repository.GetById(id);
  }

  public async Task<bool> Delete(Guid id)
  {
    return await _repository.Delete(id);
  }

  public async Task<Usuario?> Add(Usuario entity)
  {
    return await _repository.Add(entity);
  }

  public Task<Usuario> Put(Usuario entity)
  {
    return _repository.Update(entity);
  }

  public async Task<Usuario> Patch(Guid id, Usuario partialEntity)
  {
    var entity = await _repository.GetById(id);
    if (entity == null) return null;

    if (string.IsNullOrEmpty(partialEntity.Name))
      entity.Name = partialEntity.Name;
    
    await _repository.Update(entity);
    return partialEntity;
  }

  public async Task<IEnumerable<ImagesDto>> GetImages()
  {
    return await _repository.getUsersImages();
  }

}
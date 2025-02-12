using apiUsuariosCuenta.dtos;
using apiUsuariosCuenta.entities;

namespace apiUsuariosCuenta.services.Interfaces;

public interface IUserService: IService<Usuario>
{
  public Task<IEnumerable<ImagesDto>> GetImages();
}
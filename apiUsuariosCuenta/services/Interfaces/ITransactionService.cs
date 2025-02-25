using apiUsuariosCuenta.entities;

namespace apiUsuariosCuenta.services.Interfaces;

public interface ITransactionService<T> : IService<T>
{
  public Task<IEnumerable<T>> GetByUserID(Guid userId);

}
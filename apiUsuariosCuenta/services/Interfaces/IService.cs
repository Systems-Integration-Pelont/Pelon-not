namespace apiUsuariosCuenta.services.Interfaces;

public interface IService<T>
{
  public Task<IEnumerable<T>> GetAll();
  public Task<T?> GetById(Guid id);
  public Task<bool> Delete(Guid id);
  public Task<T?> Add(T entity);
  public Task<T> Put(T entity);
  public Task<T> Patch(Guid id, T entity);
}
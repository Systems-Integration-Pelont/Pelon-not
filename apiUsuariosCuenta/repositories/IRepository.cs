namespace apiUsuariosCuenta.repositories;

public interface IRepository<T>
{
  public Task<IEnumerable<T>> GetAll();
  public Task<T?> GetById(Guid id);
  public Task<bool> Delete(Guid id);
  public Task<T> Add(T entity);
  public Task<T> Update(T entity);

}
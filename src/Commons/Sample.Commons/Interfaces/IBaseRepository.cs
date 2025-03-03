namespace Sample.Commons.Interfaces;

public interface IBaseRepository<TEntity> : IDisposable where TEntity : class
{
    Task<TEntity> AddAsync(TEntity entity);
    TEntity Update(TEntity entity);
    void Delete(TEntity entity);
    void DeleteRange(IEnumerable<TEntity> entities);
}

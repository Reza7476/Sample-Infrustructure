using Microsoft.EntityFrameworkCore;
using Sample.Commons.Interfaces;
using Sample.Persistence.EF.DbContexts;

namespace Sample.Persistence.EF.EntitiesConfig;

public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
{
    private readonly EFDataContext _context;
    private readonly DbSet<TEntity> _entity;


    public BaseRepository(EFDataContext context)
    {
        _entity = context.Set<TEntity>();
        _context = context;
    }

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        await _entity.AddAsync(entity);
        return entity;
    }

    public void Delete(TEntity entity)
    {
        _entity.Remove(entity);
    }

    public void DeleteRange(IEnumerable<TEntity> entities)
    {
        _entity.RemoveRange(entities);
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    public TEntity Update(TEntity entity)
    {
        try
        {
            _context.Set<TEntity>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;

            return entity;
        }
        catch (Exception)
        {
            throw;
        }
    }
}


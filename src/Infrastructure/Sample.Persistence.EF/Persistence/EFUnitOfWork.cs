using Sample.Commons.UnitOfWork;

namespace Sample.Persistence.EF.Persistence;

public class EFUnitOfWork : IUnitOfWork
{
    private readonly EFDataContext _dataContext;
    public EFUnitOfWork(EFDataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task Begin()
    {
        await _dataContext.Database.BeginTransactionAsync();
    }

    public async Task Commit()
    {
        await _dataContext.SaveChangesAsync();
        await _dataContext.Database.CommitTransactionAsync();
    }

    public async Task Complete()
    {
        await _dataContext.SaveChangesAsync();
    }

    public async Task RoleBack()
    {
        await _dataContext.Database.RollbackTransactionAsync();
    }
}
using Sample.Commons.UnitOfWork;

namespace Sample.Persistence.EF.DbContexts;

public class EFUnitOfWork : IUnitOfWork
{
    private readonly EFDataContext _context;
    public EFUnitOfWork(EFDataContext dataContext)
    {
        _context = dataContext;
    }

    public async Task Begin()
    {
        await _context.Database.BeginTransactionAsync();
    }

    public async Task Commit()
    {
        await _context.SaveChangesAsync();
        await _context.Database.CommitTransactionAsync();
    }

    public async Task Complete()
    {
        await _context.SaveChangesAsync();
    }

    public async Task RoleBack()
    {
        await _context.Database.RollbackTransactionAsync();
    }
}
using Sample.Application.Interfaces;
using Sample.Application.Medias.Services;
using Sample.Application.Users.Services;
using Sample.Persistence.EF.EntitiesConfig.Medias;
using Sample.Persistence.EF.EntitiesConfig.Users;

namespace Sample.Persistence.EF.DbContexts;

public class EFUnitOfWork : IUnitOfWork
{
    private readonly EFDataContext _context;
    private IUserRepository? _userRepository;
    private IMediaRepository? _mediaRepository;
    public EFUnitOfWork(EFDataContext dataContext)
    {
        _context = dataContext;
     
    }

    public IUserRepository UserRepository => _userRepository ?? new EFUserRepository(_context);

    public IMediaRepository MediaRepository => _mediaRepository ?? new EFMediaRepository(_context);

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
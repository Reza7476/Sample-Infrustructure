using Microsoft.EntityFrameworkCore;
using Sample.Application.Users.Dtos;
using Sample.Application.Users.Services;
using Sample.Commons.Interfaces;
using Sample.Core.Entities.Users;
using Sample.Persistence.EF.DbContexts;
using Sample.Persistence.EF.Extensions.Paginations;


namespace Sample.Persistence.EF.EntitiesConfig.Users;

public class EFUserRepository : BaseRepository<User>, IUserRepository
{

    private readonly DbSet<User> _users;
    public EFUserRepository(EFDataContext context) : base(context)
    {
        _users = context.Set<User>();
    }

    public async Task<IPageResult<GetAllUsersDto>> GetAllUsers(IPagination? pagination = null)
    {
        var query = _users.Select(_ => new GetAllUsersDto()
        {
            Email = _.Email,
            Id = _.Id,
            Name = _.FirstName,
        }).AsQueryable();


        return await query.Paginate(pagination ?? new Pagination());
    }

    public async Task<User?> GetUserAndMediaById(long id)
    {
        return await _users
            .Where(_ => _.Id == id)
            .Include(_ => _.Medias)
            .FirstOrDefaultAsync();
    }


    public async Task<long?> GetUserIdByMacId(string mac_Id)
    {
        return await _users
            .Where(_ => _.MacId == mac_Id)
            .Select(_ => _.Id)
            .FirstOrDefaultAsync();
    }

    public async Task<bool> IsExistByMobile(string mobile)
    {
        return await _users.AnyAsync(_ => _.Mobile == mobile);
    }
}

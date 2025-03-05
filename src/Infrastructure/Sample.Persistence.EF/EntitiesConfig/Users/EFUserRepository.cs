using Microsoft.EntityFrameworkCore;
using Sample.Application.Users.Dtos;
using Sample.Application.Users.Services;
using Sample.Commons.Interfaces;
using Sample.Core.Entities.Medias;
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
            Id = _.Id,
            Name = _.FirstName,
            Email = _.Email,
        }).AsQueryable();


        return await query.Paginate(pagination ?? new Pagination());
    }

    public async Task<GetUserInfoByIdDto?> GetById(long id)
    {
        var query = await _users.Include(_ => _.Medias)
           .Where(_ => _.Id == id)
           .Select(_ => new GetUserInfoByIdDto()
           {
               Email = _.Email,
               FirstName = _.FirstName,
               LastName = _.LastName,
               Gender = _.Gender,
               ProfileUrl = _.Medias
                .FirstOrDefault(media => media.TargetType == MediaTargetType.UserProfile_Image) != null ? _.Medias
                .First(media => media.TargetType == MediaTargetType.UserProfile_Image)
                .GetURL() : null
           }).FirstOrDefaultAsync();

        return query;

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

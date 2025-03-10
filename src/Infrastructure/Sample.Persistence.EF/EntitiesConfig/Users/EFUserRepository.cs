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

    public async Task<IPageResult<GetAllUsersDto>> GetAllUsers(
        IPagination? pagination = null,
        GetAllUserFilterDto? filter = null,
        string? search = null)
    {
        var query = _users.Select(_ => new GetAllUsersDto()
        {
            Id = _.Id,
            FirstName = _.FirstName,
            LastName = _.LastName,
            Email = _.Email,
        }).AsQueryable();

        query = SearchAllUsers(search, query);
        query = FilterAllUsers(filter, query);

        return await query.Paginate(pagination ?? new Pagination());
    }


    private static IQueryable<GetAllUsersDto> FilterAllUsers(GetAllUserFilterDto? filter, IQueryable<GetAllUsersDto> query)
    {
        if (filter != null)
        {
            if (!string.IsNullOrEmpty(filter.FirstName))
            {
                query = query.Where(_ => _.FirstName.Contains(filter.FirstName));
            }

            if (!string.IsNullOrEmpty(filter.LastName))
            {
                query = query.Where(_ => _.LastName.Contains(filter.LastName));
            }
        }

        return query;
    }

    private static IQueryable<GetAllUsersDto> SearchAllUsers(string? search, IQueryable<GetAllUsersDto> query)
    {
        if (!string.IsNullOrEmpty(search))
        {

            var searchTerms = search.Trim().ToLower().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            query = query.Where(_ => searchTerms
            .Any(term => _.FirstName.ToLower().Contains(term)
                      || _.LastName.ToLower().Contains(term)));
        }

        return query;
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

    public async Task<List<User>> GetUserNotSetGender()
    {
        return await _users
            .Where(_ => _.Gender == Gender.NotSet).ToListAsync();
    }

    public async Task<bool> IsExistByMobile(string mobile)
    {
        return await _users.AnyAsync(_ => _.Mobile == mobile);
    }
}

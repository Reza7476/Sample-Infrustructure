using Sample.Application.Medias.Dtos;
using Sample.Application.Users.Dtos;
using Sample.Commons.Interfaces;

namespace Sample.Application.Users.Services;

public interface IUserService : IService
{
    Task AddUser(string name, string lastName, string mobile);
    Task<long> CreateAsync(UserInfoResponseDto dto);
    
    Task<IPageResult<GetAllUsersDto>> GetAllUsers(
        IPagination? pagination = null,
        GetAllUserFilterDto? filter = null,
        string? search = null);
    
    Task<GetUserInfoByIdDto?> GetById(long id);
    Task SetUserStatus();
}

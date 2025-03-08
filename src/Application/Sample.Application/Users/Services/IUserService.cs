using Sample.Application.Medias.Dtos;
using Sample.Application.Users.Dtos;
using Sample.Commons.Interfaces;

namespace Sample.Application.Users.Services;

public interface IUserService : IService
{
    Task AddUser(string name);
    Task<long> CreateAsync(UserInfoResponseDto dto);
    Task<IPageResult<GetAllUsersDto>> GetAllUsers(IPagination? pagination = null);
    Task<GetUserInfoByIdDto?> GetById(long id);
    Task SetUserStatus();
}

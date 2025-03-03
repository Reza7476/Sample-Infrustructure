using Sample.Application.Medias.Dtos;
using Sample.Application.Users.Dtos;
using Sample.Commons.Interfaces;

namespace Sample.Application.Users.Services;

public interface IUserService : IService
{
    Task<long> Add(AddUserDto dto);
    Task<int> CreateAsync(UserInfoResponseDto userModel);
    Task<IPageResult<GetAllUsersDto>> GetAllUsers(IPagination? pagination = null);
}

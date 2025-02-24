using Sample.Application.Users.Dtos;
using Sample.Commons.Interfaces;

namespace Sample.Application.Users.Services;

public interface IUserService : IService
{
    Task<long> Add(AddUserDto dto);

    Task<IPageResult<GetAllUsersDto>> GetAllUsers(IPagination? pagination = null);
}

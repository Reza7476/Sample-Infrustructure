using Sample.Application.Users.Dtos;
using Sample.Commons.Interfaces;
using Sample.Core.Entities.Users;

namespace Sample.Application.Users.Services;

public interface IUserRepository : IBaseRepository<User>, IRepository
{
    Task<IPageResult<GetAllUsersDto>> GetAllUsers(IPagination? pagination = null);
    Task<GetUserInfoByIdDto?> GetById(long id);
    Task<User?> GetUserAndMediaById(long id);
    Task<long?> GetUserIdByMacId(string mac_Id);
    Task<List<User>> GetUserNotSetGender();
    Task<bool> IsExistByMobile(string mobile);
}
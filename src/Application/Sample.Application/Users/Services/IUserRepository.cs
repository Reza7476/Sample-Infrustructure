using Sample.Application.Users.Dtos;
using Sample.Commons.Interfaces;
using Sample.Core.Entities.Users;

namespace Sample.Application.Users.Services;

public interface IUserRepository : IRepository
{
    Task<IPageResult<GetAllUsersDto>> GetAllUsers(IPagination? pagination=null);
    Task<User?> GetUserAndMediaById(long id);
    Task<int?> GetUserIdByMacId(string mac_Id);
   
    Task Add(User user);
    Task<bool> IsExistByMobile(string mobile);
}
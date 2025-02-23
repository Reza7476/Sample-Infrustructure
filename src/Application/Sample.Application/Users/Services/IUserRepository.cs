using Sample.Application.Users.Dtos;
using Sample.Commons.Interfaces;
using Sample.Core.Entities.Users;

namespace Sample.Application.Users.Services;

public interface IUserRepository : IRepository
{
    Task Add(User user);
    Task<List<GetAllUsersDto>> GetAllUsers(IPagination? pagination=null);
}
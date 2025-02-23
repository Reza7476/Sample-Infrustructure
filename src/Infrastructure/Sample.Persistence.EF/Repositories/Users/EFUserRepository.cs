using Microsoft.EntityFrameworkCore;
using Sample.Application.Users.Dtos;
using Sample.Application.Users.Services;
using Sample.Commons.Interfaces;
using Sample.Core.Entities.Users;
using Sample.Persistence.EF.Persistence;

namespace Sample.Persistence.EF.Repositories.Users;

public class EFUserRepository : IUserRepository
{

    private readonly DbSet<User> _users;

    public EFUserRepository(EFDataContext context)
    {
        _users = context.Set<User>();
    }

    public async Task Add(User user)
    {
       await _users.AddAsync(user); 
    }

    public async Task<List<GetAllUsersDto>> GetAllUsers(IPagination? pagination = null)
    {
        return await _users .Select(_=>new GetAllUsersDto()
        {
            Email=_.Email,
            Id=_.Id,
            Name=_.Name,
        }).ToListAsync();
    }
}

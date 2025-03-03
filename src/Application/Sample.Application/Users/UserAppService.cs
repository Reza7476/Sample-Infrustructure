using Sample.Application.Medias.Dtos;
using Sample.Application.Users.Dtos;
using Sample.Application.Users.Exceptions;
using Sample.Application.Users.Services;
using Sample.Commons.Interfaces;
using Sample.Commons.UnitOfWork;
using Sample.Core.Entities.Medias;
using Sample.Core.Entities.Users;

namespace Sample.Application.Users;

public class UserAppService : IUserService
{
    private readonly IUserRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public UserAppService(
        IUserRepository repository,
        IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<long> CreateAsync(UserInfoResponseDto dto)
    {
        var user = new User()
        {
            Email = dto.Email,
            FirstName = dto.Name,
            LastName = dto.Family,
            MacId = dto.Sub,
            Mobile = dto.PhoneNumber,
            Gender = Gender.NotSet,
        };
        await _repository.AddAsync(user);
        await _unitOfWork.Complete();
        return user.Id;
    }

    public async Task<IPageResult<GetAllUsersDto>> GetAllUsers(IPagination? pagination = null)
    {
        return await _repository.GetAllUsers(pagination);
    }
   
}

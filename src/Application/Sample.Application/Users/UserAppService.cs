using Sample.Application.Users.Dtos;
using Sample.Application.Users.Exceptions;
using Sample.Application.Users.Services;
using Sample.Commons.Interfaces;
using Sample.Commons.UnitOfWork;
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

    public async Task<long> Add(AddUserDto dto)
    {
        await StopIfMobileIsDuplicate(dto);

        var user = new User
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            Mobile = dto.Mobile,

        };

        await _repository.Add(user);
        await _unitOfWork.Complete();

        return user.Id;
    }

    public async Task<IPageResult<GetAllUsersDto>> GetAllUsers(IPagination? pagination = null)
    {
        return await _repository.GetAllUsers(pagination);
    }

    private async Task StopIfMobileIsDuplicate(AddUserDto dto)
    {
        if (await _repository.IsExistByMobile(dto.Mobile))
        {
            throw new MobilIsDuplicateException();
        }
    }
}

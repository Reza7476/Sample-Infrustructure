using Sample.Application.Interfaces;
using Sample.Application.Users.Dtos;
using Sample.Application.Users.Exceptions;
using Sample.Application.Users.Services;
using Sample.Commons.Interfaces;
using Sample.Core.Entities.Users;
using Sample.Persistence.EF.Extensions;

namespace Sample.Application.Users;

public class UserAppService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;

    public UserAppService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task AddUser(AddUserDto dto)
    {
        StopIfNationalCodeIsInvalid(dto);

        var user = new User()
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Mobile = dto.Mobile,
            NationalCode = dto.NationalCode,
            Gender = Gender.NotSet,
        };

        await _unitOfWork.UserRepository.AddAsync(user);
        await _unitOfWork.Complete();

    }

    private static void StopIfNationalCodeIsInvalid(AddUserDto dto)
    {
        if (dto.NationalCode != null && !dto.NationalCode.NationalCode())
        {
            throw new NationalCodeIsInvalidException();
        }
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
        await _unitOfWork.UserRepository.AddAsync(user);
        await _unitOfWork.Complete();
        return user.Id;
    }

    public async Task<IPageResult<GetAllUsersDto>> GetAllUsers(
        IPagination? pagination = null,
        GetAllUserFilterDto? filter = null,
        string? search = null)
    {
        return await _unitOfWork.UserRepository.GetAllUsers(pagination, filter, search);
    }

    public async Task<GetUserInfoByIdDto?> GetById(long id)
    {
        return await _unitOfWork.UserRepository.GetById(id);
    }

    public async Task SetUserStatus()
    {
        var users = await _unitOfWork.UserRepository.GetUserNotSetGender();

        foreach (var user in users)
        {
            user.UserStatus = UserStatus.NOK;
        }

        await _unitOfWork.Complete();
    }
}

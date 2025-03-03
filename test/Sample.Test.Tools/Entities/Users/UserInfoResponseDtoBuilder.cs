using Sample.Application.Users.Dtos;
using System.Security.AccessControl;

namespace Sample.Test.Tools.Entities.Users;

public class UserInfoResponseDtoBuilder
{
    private readonly UserInfoResponseDto _dto;

    public UserInfoResponseDtoBuilder()
    {
        _dto = new UserInfoResponseDto()
        {
            Name = "name",
            Family = "family",
            PhoneNumber = "+989174367476",
            UserName = "user-name",
            Sub = "sub",
            Email = "Reza@Gmail.com",
            Roles = new()
        };
    }


    public UserInfoResponseDtoBuilder WithMobile(string mobile)
    {
        _dto.PhoneNumber = mobile;
        return this;
    }


    public UserInfoResponseDtoBuilder WithSub(string sub)
    {
        _dto.Sub = sub;
        return this;
    }

    public UserInfoResponseDtoBuilder WithName(string name)
    {
        _dto.Name = name;
        return this;
    }


    public UserInfoResponseDtoBuilder WithLastName(string lastName)
    {
        _dto.Family = lastName;
        return this;
    }

    public UserInfoResponseDto Build()
    {
        return _dto;
    }

}

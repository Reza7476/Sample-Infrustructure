using Sample.Application.Users.Dtos;
using Sample.Core.Entities.Users;

namespace Sample.Test.Tools.Entities.Users;

public class AddUserDtoBuilder
{
    private readonly AddUserDto _dto;

    public AddUserDtoBuilder()
    {
        _dto = new AddUserDto()
        {
            Email = "Email",
            FirstName = "Name",
            LastName = "LastName",
            Mobile = "9174367476"
        };
    }
    
    public AddUserDtoBuilder WithFirstName(string name)
    {
        _dto.FirstName = name;
        return this;
    }

    public AddUserDtoBuilder WithLastName(string lastName)
    {
        _dto.LastName = lastName;
        return this;
    }

    public AddUserDtoBuilder WithGender(Gender gender)
    {
        _dto.Gender = gender;
        return this;
    }
    
    public AddUserDtoBuilder WithMobile(string mobile)
    {
        _dto.Mobile = mobile;
        return this;
    }

    public AddUserDtoBuilder WithNationalCode(string nationalCode)
    {
        _dto.NationalCode = nationalCode;   
        return this;    
    }

    public AddUserDtoBuilder WithEmail(string email)
    {
        _dto.Email = email;
        return this;
    }

    public AddUserDto Build()
    {
        return _dto;
    }
}
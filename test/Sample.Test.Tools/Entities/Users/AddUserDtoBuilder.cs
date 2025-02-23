using Sample.Application.Users.Dtos;

namespace Sample.Test.Tools.Entities.Users;

public class AddUserDtoBuilder
{
    private readonly AddUserDto _dto;

    public AddUserDtoBuilder()
    {
        _dto = new AddUserDto()
        {
            Email = "Email",
            Name = "Name",
        };
    }

    public AddUserDtoBuilder WithEmail(string email)
    {
        _dto.Email = email;
        return this;
    }

    public AddUserDtoBuilder WithName(string name)
    {
        _dto.Name = name;
        return this;
    }

    public AddUserDto Build()
    {
        return _dto;
    }
}
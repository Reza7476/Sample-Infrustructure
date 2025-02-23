using Sample.Core.Entities.Users;

namespace Sample.Test.Tools.Entities.Users;

public class UserBuilder
{
    private readonly User _user;

    public UserBuilder()
    {
        _user = new User()
        {
            Email = "email",
            Name = "namme"
        };
    }

    public UserBuilder WithName(string name)
    {
        _user.Name = name;
        return this;
    }

    public UserBuilder WithEmail(string email)
    {
        _user.Email = email;
        return this;
    }

    public User Build()
    {
        return _user;
    }
}


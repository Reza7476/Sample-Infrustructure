using FluentAssertions;
using Sample.Application.Users.Services;
using Sample.Test.Tools.Entities.Users;
using Sample.Test.Tools.Infrastructure.DataBaseConfig.Integration;

namespace Sample.Application.Test.Integration.Users;

public class UserServiceTests : BusinessIntegrationTest
{
    private readonly IUserService _sut;

    public UserServiceTests()
    {
        _sut = UserServiceFactory.Create(SetupContext);
    }

    [Fact]
    public async Task GetAllUsers_should_return_all_user_properly()
    {
        var user = new UserBuilder()
            .WithEmail("email")
            .WithFirstName("name")
            .Build();
        Save(user);

        var expected = await _sut.GetAllUsers();

        expected.Elements.First().Email.Should().Be(user.Email);
        expected.Elements.First().Name.Should().Be(user.FirstName);
        expected.Elements.First().Id.Should().Be(user.Id);
    }
}

using FluentAssertions;
using Sample.Application.Users.Services;
using Sample.Core.Entities.Users;
using Sample.Test.Tools.Entities.Users;
using Sample.Test.Tools.Infrastructure.DataBaseConfig.Integration;

namespace Sample.Application.Test.Integration.Users;

public class UserServiceTests: BusinessIntegrationTest
{
    private readonly IUserService _sut;

    public UserServiceTests()
    {
        _sut = UserServiceFactory.Create(SetupContext);
    }

    [Fact]
    public async Task Add_should_add_user_properly()
    {
        var dto = new AddUserDtoBuilder()
            .WithName("Name")
            .WithEmail("Email")
            .Build();

        await _sut.Add(dto);

        var expected = ReadContext.Set<User>().First();
        expected.Name.Should().Be(dto.Name);
        expected.Email.Should().Be(dto.Email);
    }
}

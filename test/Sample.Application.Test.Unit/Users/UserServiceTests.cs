using FluentAssertions;
using Sample.Application.Users.Services;
using Sample.Core.Entities.Users;
using Sample.Test.Tools.Entities.Users;
using Sample.Test.Tools.Infrastructure.DataBaseConfig.Unit;

namespace Sample.Application.Test.Unit.Users;

public class UserServiceTests : BusinessUnitTest
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
            .WithName("Reza")
            .WithEmail("Email")
            .Build();

        await _sut.Add(dto);

        var expected = ReadContext.Set<User>().First();
        expected.Name.Should().Be(dto.Name);
        expected.Email.Should().Be(dto.Email);
    }

    [Fact]
    public async Task GetAllUsers_should_return_all_user_properly()
    {
        var user = new UserBuilder()
            .WithEmail("email")
            .WithName("name")
            .Build();
        Save(user);
        
        var expected = await _sut.GetAllUsers();

        expected.First().Email.Should().Be(user.Email); 
        expected.First().Name.Should().Be(user.Name);   
        expected.First().Id.Should().Be(user.Id);
    }
}

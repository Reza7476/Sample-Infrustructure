using FluentAssertions;
using Sample.Application.Users.Exceptions;
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
            .WithFirstName("Reza")
            .WithEmail("Email")
            .Build();

        await _sut.Add(dto);

        var expected = ReadContext.Set<User>().First();
        expected.FirstName.Should().Be(dto.FirstName);
        expected.Email.Should().Be(dto.Email);
    }

    [Fact]
    public async Task Add_should_throw_exception_when_moblile_is_duplicat()
    {
        var user = new UserBuilder()
            .WithMobile("+989174367476")
            .Build();
        Save(user);
        var dto=new AddUserDtoBuilder()
            .WithMobile("+989174367476")
            .Build();

        Func<Task> expected=async()=>await _sut.Add(dto);

        await expected.Should().ThrowAsync < MobilIsDuplicateException>();
    }

}

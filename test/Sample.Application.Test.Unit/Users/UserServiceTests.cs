using FluentAssertions;
using Sample.Application.Users.Services;
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
    public  async Task CreateUser_should_create_user_properly()
    {
        var dto = new UserInfoResponseDtoBuilder()
            .WithSub("sub")
            .WithMobile("+989174367476")
            .WithLastName("reza")
            .WithName("Dehghani")
            .Build();
        await _sut.CreateAsync(dto);

        var expected = ReadContext.Users.FirstOrDefault();
        expected!.FirstName.Should().Be(dto.Name);
        expected.LastName.Should().Be(dto.Family);
        expected.Mobile.Should().Be(dto.PhoneNumber);
        expected.MacId.Should().Be(dto.Sub);

    }
}

using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Sample.Application.Users.Exceptions;
using Sample.Application.Users.Services;
using Sample.Core.Entities.Users;
using Sample.Test.Tools.Entities.Medias;
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
        var dto = new AddUserDtoBuilder()
            .WithMobile("+989174367476")
            .Build();

        Func<Task> expected = async () => await _sut.Add(dto);

        await expected.Should().ThrowAsync<MobilIsDuplicateException>();
    }


    [Fact]
    public async Task AddProfileImage_should_add_user_profile_image_properly()
    {
        var user = new UserBuilder()
            .WithMobile("9174367476")
            .Build();
        Save(user);
        var dto = new AddMediaDtoBuilder()
            .WithFile()
            .Build();
        await _sut.AddProfileImage(dto, user.Id);

        var expected = ReadContext.Set<User>().Include(_ => _.Medias).First();
        expected.Medias.First().Extension.Should().Bto.



    }

}

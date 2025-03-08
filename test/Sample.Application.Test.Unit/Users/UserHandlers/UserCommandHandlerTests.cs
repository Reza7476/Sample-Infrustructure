using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;
using Sample.Application.Interfaces;
using Sample.Application.Medias.Dtos;
using Sample.Application.Medias.Services;
using Sample.Application.Users.Services;
using Sample.Application.Users.UserHandlers;
using Sample.Core.Entities.Medias;
using Sample.Persistence.EF.DbContexts;
using Sample.Test.Tools.Entities.Medias;
using Sample.Test.Tools.Entities.Users;
using Sample.Test.Tools.Infrastructure.DataBaseConfig.Unit;
using Sample.Test.Tools.Infrastructure.DummyData;

namespace Sample.Application.Test.Unit.Users.UserHandlers;

public class UserCommandHandlerTests : BusinessUnitTest
{
    private readonly IUserHandler _sut;
    private readonly IUserService _userService;
    private readonly Mock<IMediaService> _mediaServiceMock;
    private readonly IUnitOfWork _unitOfWork;

    public UserCommandHandlerTests()
    {
        _userService = UserServiceFactory.Create(SetupContext);
        _mediaServiceMock = new Mock<IMediaService>();
        _unitOfWork = new EFUnitOfWork(SetupContext);

        _sut = new UserCommandHandler(
            _mediaServiceMock.Object,
            _userService,
            _unitOfWork);
    }

    [Theory]
    [DummyFormFile]
    public async Task AddProfileImage_should_add_user_profile_properly(IFormFile file)
    {
        var user = new UserBuilder()
            .Build();
        Save(user);
        var dto = new AddMediaDto()
        {
            File = file
        };
        var media = new MediaBuilder()
            .WithUserId(user.Id)
            .Build();
        _mediaServiceMock.Setup(_ => _.CreateMediaModelFromFile(It.IsAny<CreateMediaDto>()))
            .ReturnsAsync(media);

        await _sut.AddProfileImage(dto, user.Id);

        var expected = ReadContext.Set<Media>().FirstOrDefault();
        expected.Should().NotBeNull();
        expected!.Title.Should().Be(media.Title);
        expected.UniqueName.Should().Be(media.UniqueName);
        expected.ThumbnailUniqueName.Should().Be(media.ThumbnailUniqueName);
        expected.URL.Should().Be(media.URL);
        expected.Extension.Should().Be(media.Extension);
        expected.Type.Should().Be(media.Type);
        expected.CreatedAt.Should().Be(media.CreatedAt);
        expected.UserId.Should().Be(user.Id);
    }
}

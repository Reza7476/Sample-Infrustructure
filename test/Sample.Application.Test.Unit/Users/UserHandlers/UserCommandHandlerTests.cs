using Moq;
using Sample.Application.Medias.Services;
using Sample.Application.Users.Services;
using Sample.Application.Users.UserHandlers;
using Sample.Test.Tools.Entities.Users;
using Sample.Test.Tools.Infrastructure.DataBaseConfig.Unit;

namespace Sample.Application.Test.Unit.Users.UserHandlers;

public class UserCommandHandlerTests : BusinessUnitTest
{
    private readonly IUserHandler _sut;
    private readonly IUserService _userService;
    private readonly Mock<IMediaService> _mediaServiceMock;
    public UserCommandHandlerTests()
    {
        _userService = UserServiceFactory.Create(SetupContext);
        _mediaServiceMock = new Mock<IMediaService>();

        _sut = new UserCommandHandler(_mediaServiceMock.Object, _userService);
    }


}

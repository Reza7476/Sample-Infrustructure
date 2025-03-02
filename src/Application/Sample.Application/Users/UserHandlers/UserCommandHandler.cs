using Sample.Application.Medias.Services;
using Sample.Application.Users.Services;

namespace Sample.Application.Users.UserHandlers;

public class UserCommandHandler : IUserHandler
{
    private readonly IUserService _userService;
    private readonly IMediaService _mediaService;

    public UserCommandHandler(
        IMediaService mediaService,
        IUserService userService)
    {
        _mediaService = mediaService;
        _userService = userService;
    }




}

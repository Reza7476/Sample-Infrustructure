using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sample.Application.Medias.Dtos;
using Sample.Application.Users.Dtos;
using Sample.Application.Users.Services;
using Sample.Application.Users.UserHandlers;
using Sample.Commons.Interfaces;

namespace Sample.RestApi.Controllers.Users;
[Route("api/users")]
[ApiController]
//[Authorize]
public class UsersController : ControllerBase
{
    private readonly IUserService _service;
    private readonly IUserTokenService _userToken;
    private readonly IUserHandler _userHandlerService;

    public UsersController(IUserService service,
        IUserTokenService userToken,
        IUserHandler userHandlerService)
    {
        _service = service;
        _userToken = userToken;
        _userHandlerService = userHandlerService;
    }

    [HttpPost]
    public async Task<long> Add(AddUserDto dto)
    {
        return await _service.Add(dto);
    }

    [HttpGet("all")]
    public Task<IPageResult<GetAllUsersDto>> GetAllUsers()
    {
        return _service.GetAllUsers();
    }


    [HttpPut("add-profile-image")]
    //[Authorize]
    public async Task AddProfileImage([FromForm] AddMediaDto dto)
    {
        //var userId = _userToken.UserId;
       var userId = 1;
        await _userHandlerService.AddProfileImage(dto, userId);
    }
}

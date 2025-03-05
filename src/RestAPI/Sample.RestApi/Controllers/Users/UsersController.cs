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
 
    [HttpGet("all")]
    public Task<IPageResult<GetAllUsersDto>> GetAllUsers()
    {
        return _service.GetAllUsers();
    }

    [HttpPost("add-profile-image")]
    public async Task AddProfileImage([FromForm] AddMediaDto dto)
    {
        //var userId = _userToken.UserId;
       var userId = 1;
        await _userHandlerService.AddProfileImage(dto, userId);
    }

    [HttpPut("update-profile-image")]

    public async Task UpdateProfileImage([FromForm ] AddMediaDto dto)
    {
        //var userId = _userToken.UserId;
        var userId = 1;

        await _userHandlerService.UpdateProfileImage(dto, userId);
    }

    [HttpGet("get-by-id")]
    public async Task <GetUserInfoByIdDto?> GetUserById(long id)
    {
        return await _service.GetById(id);
    }

    [HttpPost("add-user")]
    public async Task AddUser( string name)
    {
        await _service.AddUser(name);
    }

}

﻿using Microsoft.AspNetCore.Mvc;
using Sample.Application.Users.Dtos;
using Sample.Application.Users.Services;
using Sample.Commons.Interfaces;

namespace Sample.RestApi.Controllers.Users;
[Route("api/users")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserService _service;

    public UsersController(IUserService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task <long> Add(AddUserDto dto)
    {
        return await _service.Add(dto);
    }
    
    [HttpGet("all")]
    public Task <IPageResult<GetAllUsersDto>> GetAllUsers()
    {
        return _service.GetAllUsers();
    }
   
}

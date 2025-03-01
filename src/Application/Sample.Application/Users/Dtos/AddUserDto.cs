using Microsoft.AspNetCore.Http;
using Sample.Commons.Contracts;
using Sample.Core.Entities.Users;

namespace Sample.Application.Users.Dtos;

public class AddUserDto
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Mobile { get; set; }
    public required string Email { get; set; }
    [EnumValueChecker]
    public Gender? Gender { get; set; }
    public string? NationalCode { get; set; }
   // public IFormFile? ProfileImage { get; set; }
}

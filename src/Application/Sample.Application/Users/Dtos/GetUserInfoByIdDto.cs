using Sample.Core.Entities.Users;

namespace Sample.Application.Users.Dtos;

public class GetUserInfoByIdDto
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? ProfileUrl { get; set; }
    public Gender Gender { get; set; }
}

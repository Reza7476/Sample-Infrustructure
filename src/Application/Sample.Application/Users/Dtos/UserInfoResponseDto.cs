namespace Sample.Application.Users.Dtos;

public class UserInfoResponseDto
{
    public required string Sub { get; set; }
    public required string Name { get; set; }
    public required string Family { get; set; }
    public string? Email { get; set; }
    public string? UserName { get; set; }
    public string? PhoneNumber { get; set; }
    public List<string> Roles { get; set; } = [];
}

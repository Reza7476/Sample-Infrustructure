namespace Sample.Application.Users.Dtos;

public class GetAllUsersDto
{
    public long Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
}

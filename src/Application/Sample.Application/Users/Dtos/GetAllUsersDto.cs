namespace Sample.Application.Users.Dtos;

public class GetAllUsersDto
{
    public long Id { get; set; }
    public  required string FirstName { get; set; }
    public required string LastName { get; set; }
    public  string? Email { get; set; }
}

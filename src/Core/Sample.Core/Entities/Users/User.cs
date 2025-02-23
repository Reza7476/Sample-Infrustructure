namespace Sample.Core.Entities.Users;

public class User
{
    public long Id { get; set; }
    public string Name { get; set; } = default!;
    public string Email { get; set; } = default!;
}


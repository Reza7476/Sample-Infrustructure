using Sample.Core.Entities.Generals;

namespace Sample.Core.Entities.Users;

public class User : BaseEntity
{
    public required string Name { get; set; } 
    public required string Email { get; set; } 
}


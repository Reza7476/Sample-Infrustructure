using Sample.Core.Entities.Generals;

namespace Sample.Core.Entities.Users;

public class User : BaseEntity
{
    public string Name { get; set; } = default!;
    public string Email { get; set; } = default!;
}


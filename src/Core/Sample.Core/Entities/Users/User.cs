using Sample.Core.Entities.Generals;

namespace Sample.Core.Entities.Users;

public class User : BaseEntity
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public required string Mobile { get; set; }
    public string? ProfileUrl { get; set; }
    public Gender Gender { get; set; }
    public string? NationalCode { get; set; }

}

public enum Gender
{
    Male = 1,
    Female = 2
}


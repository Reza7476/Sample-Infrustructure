using Sample.Core.Entities.Generals;
using Sample.Core.Entities.Medias;

namespace Sample.Core.Entities.Users;

public class User : BaseEntity
{
    public User()
    {
        Medias = new HashSet<Media>();
    }

    public string? MacId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? Mobile { get; set; }
    public string? ProfileUrl { get; set; }
    public Gender Gender { get; set; }
    public string? NationalCode { get; set; }
    public HashSet<Media> Medias { get; set; }


    public void AddMedia(Media media)
    {
        media.UserId = this.Id;
    }

    public string GetFullName()
    {
        return this.FirstName + " " + this.LastName;
    }
}

public enum Gender
{
    Male = 1,
    Female = 2,
    NotSet = 3,
}


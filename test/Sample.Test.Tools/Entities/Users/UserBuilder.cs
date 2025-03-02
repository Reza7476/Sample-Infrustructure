using Sample.Core.Entities.Medias;
using Sample.Core.Entities.Users;

namespace Sample.Test.Tools.Entities.Users;

public class UserBuilder
{
    private readonly User _user;

    public UserBuilder()
    {
        _user = new User()
        {
            Email = "email",
            FirstName = "name",
            LastName = "lastName",
            Mobile = "+989174367476",
            Gender = Gender.Male,
            NationalCode = "nationalCode",
            ProfileUrl = "url/profile",
            MacId = new Guid().ToString(),
            Medias = new HashSet<Media>()

        };
    }

    public UserBuilder WithFirstName(string name)
    {
        _user.FirstName = name;
        return this;
    }

    public UserBuilder WithLastName(string lastName)
    {
        _user.LastName = lastName;
        return this;
    }

    public UserBuilder WithEmail(string email)
    {
        _user.Email = email;
        return this;
    }

    public UserBuilder WithMobile(string mobile)
    {
        _user.Mobile = mobile;
        return this;
    }

    public UserBuilder WithProfile(string profileImage)
    {
        _user.ProfileUrl = profileImage;
        return this;
    }

    public UserBuilder WithGender(Gender gender)
    {
        _user.Gender = gender;
        return this;
    }

    public UserBuilder WithMacId(string macId)
    {
        _user.MacId = macId;
        return this;
    }

    public UserBuilder WithMedias(
        string uniqueName,
        string url,
        string extension,
        string has,
        MediaTargetType targetType,
        MediaType mediaType,
        int? SortInOrder,
        string? altText = "alt",
        string? thumbnailUniqueName = "thumbnail",
        string? title = "title")
    {
        _user.Medias.Add(new Media()
        {
            Extension = extension,
            Hash = has,
            UniqueName = uniqueName,
            URL = url,
            AltText = altText ?? null,
            Title = title ?? null,
            SortingOrder = 1,
            TargetType = targetType,
            Type = mediaType,
            ThumbnailUniqueName = thumbnailUniqueName ?? null,
        });
        return this;
    }

    public UserBuilder WithNationalCode(string nationalCode)
    {
        _user.NationalCode = nationalCode;
        return this;
    }

    public User Build()
    {
        return _user;
    }
}


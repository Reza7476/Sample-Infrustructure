﻿using Sample.Core.Entities.Generals;
using Sample.Core.Entities.Medias;

namespace Sample.Core.Entities.Users;

public class User : BaseEntity
{
    public User()
    {
        Medias = new HashSet<Media>();
        HangFires = new HashSet<UserHangfire>();
    }

    public string? MacId { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Mobile { get; set; }
    public string? Email { get; set; }
    public string? ProfileUrl { get; set; }
    public Gender Gender { get; set; }
    public UserStatus UserStatus { get; set; }
    public string? NationalCode { get; set; }
    public HashSet<Media> Medias { get; set; }
    public HashSet<UserHangfire> HangFires { get; set; }

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

public enum UserStatus : byte
{
    NotSet=0,
    Ok = 1,
    NOK = 2
}


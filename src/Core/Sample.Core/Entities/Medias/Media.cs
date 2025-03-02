using Sample.Core.Entities.Generals;
using Sample.Core.Entities.Users;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sample.Core.Entities.Medias;
public class Media : BaseEntity
{
    public required string UniqueName { get; set; }
    public required string URL { get; set; }
    public required string Extension { get; set; }
    public required string Hash { get; set; }
    public string? Title { get; set; }
    public string? ThumbnailUniqueName { get; set; }
    public string? AltText { get; set; }
    public int SortingOrder { get; set; }
    public MediaType Type { get; set; }
    public MediaTargetType TargetType { get; set; }
    
    [NotMapped]
    public string? Main_Base64 { get; set; }
    
    [NotMapped]
    public string? Thumb_Base64 { get; set; }
    
    public string GetURL() => URL + "/" + UniqueName + Extension;
    
    public string GetThumbnailURL() => URL + "/" + ThumbnailUniqueName + Extension;
    
    public User? User { get; set; }
    
    public long? UserId { get; set; }
}

public enum MediaType : byte
{
    Image = 1,
    Voice = 2,
    Video = 3,
    Pdf = 4,
    Word = 5,
    Unknown = 6
}
public enum MediaTargetType : byte
{
    UserProfile_Image = 1,
    Category_Icon=2,
}
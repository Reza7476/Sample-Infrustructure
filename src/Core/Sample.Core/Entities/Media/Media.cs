using Sample.Core.Entities.Generals;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sample.Core.Entities.Media;
public class Media : BaseEntity
{
    public string? Title { get; set; }
    public required string UniqueName { get; set; }
    public string? ThumbnailUniqueName { get; set; }
    public required string URL { get; set; }
    public string? AltText { get; set; }
    public required string Extension { get; set; }
    public required string Hash { get; set; }
    public int SortingOrder { get; set; }
    [NotMapped]
    public string? Main_Base64 { get; set; }
    [NotMapped]
    public string? Thumb_Base64 { get; set; }
    public MediaType Type { get; set; }
    public MediaTargetType TargetType { get; set; }
    public string GetURL() => this.URL + "/" + this.UniqueName + this.Extension;
    public string GetThumbnailURL() => this.URL + "/" + this.ThumbnailUniqueName + this.Extension;
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
    Category_Icon = 1
}
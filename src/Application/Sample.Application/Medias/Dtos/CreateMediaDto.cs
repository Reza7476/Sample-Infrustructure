using Microsoft.AspNetCore.Http;
using Sample.Core.Entities.Medias;

namespace Sample.Application.Medias.Dtos;

public class CreateMediaDto
{
    public MediaTargetType TargetType { get; set; }
    public MediaType Type { get; set; }
    public string? Title { get; set; }
    public IFormFile? File { get; set; }
    public bool CreateThumbnail { get; set; }
    public bool ConvertToWebp { get; set; }
    public string? AltText { get; set; }
    public int ThumbnailWithSize { get; set; }
    public int SortOrder { get; set; }
    public Media? Media { get; set; }
}

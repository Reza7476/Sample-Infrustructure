using Microsoft.AspNetCore.Http;
using Sample.Application.Medias.Dtos;
using Sample.Application.Medias.Exceptions;
using Sample.Application.Medias.Services;
using Sample.Core.Entities.Medias;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Webp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System.Security.Cryptography;
using System.Text;
using Image = SixLabors.ImageSharp.Image;

namespace Sample.Application.Medias;

public class MediaAppService : IMediaService
{
    private static readonly Dictionary<MediaType, HashSet<string>> _validExtensions = new()

        {
        { MediaType.Image, new() { ".png", ".jpg", ".jpeg", ".gif" } },
        { MediaType.Voice, new() { ".aac", ".mp3", ".wav", ".mpeg" } },
        { MediaType.Video, new() { ".mp4", ".avi", ".mov", ".mkv" } }, // اگر لازم است
        { MediaType.Pdf, new() { ".pdf" } },
        { MediaType.Word, new() { ".docm", ".docx", ".doc", ".dot" } }
    };


    public async Task<Media> CreateMediaModelFromFile(CreateMediaDto dto)
    {
        if (dto.File == null)
        {
            throw new MediaIsNullException();
        }
        var FileSize = GetFileSizeFromFile(dto.File);
        StopIfSizeNotAcceptable(dto.Type, FileSize);
        string extension = GetFileExtension(dto.File);
        StopIfExtensionNotAcceptable(dto.Type, extension);

        if (dto.ConvertToWebp == true)
            extension = ".webp";

        string base64 = GenerateBase64FromFile(dto.File);
        var MD5_Hash = GenerateMD5Hash(base64);
        string thumbnailBase64 = string.Empty;
        if (dto.CreateThumbnail == true)
            thumbnailBase64 = await CreateThumbnail(dto.File, dto.ThumbnailWithSize);
        if (string.IsNullOrWhiteSpace(dto.Title))
            dto.Title = dto.File.FileName;

        var mediaModel = CreateMedia(type: dto.Type,
               targetType: dto.TargetType,
               extension: extension,
               hash: MD5_Hash,
               title: dto.Title,
               media: dto.Media,
               base64: base64,
               thumbBase64: thumbnailBase64,
               altText: dto.AltText);
        return mediaModel;
    }

    private static double GetFileSizeFromFile(IFormFile file)
    {
        var fileSize = file.Length;

        double sizeInKb = fileSize / 1024.0;
        return sizeInKb;
    }

    private void StopIfSizeNotAcceptable(MediaType type, double fileSize)
    {
        double maxSize = type switch
        {
            MediaType.Image => 10 * 1024,   //10MB
            MediaType.Voice => 10 * 1024,   //10MB
            MediaType.Video => 10 * 1024,   //10MB
            MediaType.Pdf => 10 * 1024,     //10MB
            _ => throw new ArgumentOutOfRangeException(nameof(type), "Unsupported media type")
        };

        if (fileSize > maxSize)
            throw new FileSizeNotAcceptableException();
    }

    private static string GetFileExtension(IFormFile file)
    {
        var fileExtension = Path.GetExtension(file.FileName).ToLower();
        return fileExtension;
    }

    private void StopIfExtensionNotAcceptable(MediaType type, string extension)
    {
        if (!_validExtensions.TryGetValue(type, out var validExts) || !validExts.Contains(extension.ToLower()))
            throw new FileExtensionNotAcceptableException();
    }


    private static string GenerateBase64FromFile(IFormFile file)
    {
        using (MemoryStream memoryStream = new MemoryStream())
        {
            file.CopyTo(memoryStream);
            byte[] fileBytes = memoryStream.ToArray();
            string base64String = Convert.ToBase64String(fileBytes);
            return base64String;
        }
    }

    private static string GenerateMD5Hash(string unHashed)
    {
        var md5 = MD5.Create();
        byte[] data = md5.ComputeHash(Encoding.UTF8.GetBytes(unHashed));
        return BitConverter.ToString(data).Replace("-", "");
    }

    private async Task<string> CreateThumbnail(IFormFile file, int? thumbnailWidthSize = null)
    {
        using (var mainImage = await Image.LoadAsync<Rgba32>(file.OpenReadStream()))
        {
            int maxPixels = 0; // Change this to your desired thumbnail size
            if (thumbnailWidthSize.HasValue)
                maxPixels = thumbnailWidthSize.Value;
            else
                maxPixels = 150;

            mainImage.Mutate(x => x.Resize(new ResizeOptions
            {
                Size = new SixLabors.ImageSharp.Size(maxPixels, maxPixels),
                Mode = ResizeMode.Max
            }));

            using (var memoryStream = new MemoryStream())
            {
                // Convert to .webp
                await mainImage.SaveAsWebpAsync(memoryStream, new WebpEncoder { Quality = 80 });
                // Save thumbnail to file
                return Convert.ToBase64String(memoryStream.ToArray());
            }
        }
    }

    private static string CreateFileURL(MediaType type, MediaTargetType targetType)
    {
        string URIAddress = "/Media/";

        switch (type)
        {
            case MediaType.Image:
                URIAddress += "Images/";
                switch (targetType)
                {
                    case MediaTargetType.Category_Icon:
                        URIAddress += "CategoryIcon";
                        break;
                    default:
                        break;
                }

                break;
            case MediaType.Voice:
                URIAddress += "Voices";

                break;
            case MediaType.Video:
                URIAddress += "Videos";

                break;
            case MediaType.Pdf:
                URIAddress += "PDFs";

                break;
            default:
                break;
        }

        return URIAddress;

    }
    private static Media CreateMedia(MediaType type,
        MediaTargetType targetType,
        string extension,
        string hash,
        string? title,
        string? base64,
        Media? media,
        string? altText,
        string? thumbBase64 = null)
    {
        if (media != null)
        {
            media.Extension = extension;
            media.Hash = hash;
            media.Title = title;
            media.Main_Base64 = base64;
            media.Thumb_Base64 = thumbBase64;
            media.AltText = altText;
            return media;
        }
        else
        {
            string uniqueName = GenerateMD5Hash(Guid.NewGuid().ToString());
            var Media = new Media()
            {
                UniqueName = uniqueName + "_main",
                ThumbnailUniqueName = string.IsNullOrWhiteSpace(thumbBase64) ? null : uniqueName + "_thumb",
                Extension = extension,
                Type = type,
                Hash = hash,
                Title = title,
                AltText = altText,
                URL = CreateFileURL(type, targetType),
                Main_Base64 = base64,
                Thumb_Base64 = thumbBase64,
                TargetType = targetType
            };
            return Media;
        }
    }
}

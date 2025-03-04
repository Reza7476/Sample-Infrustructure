using Sample.Core.Entities.Medias;

namespace Sample.Test.Tools.Entities.Medias;

public class MediaBuilder
{
    private readonly Media _media;
    public MediaBuilder()
    {
        _media = new Media()
        {
            Extension = ".jpg",
            Hash = "hash-code",
            UniqueName = "unique-Name",
            URL = "Url",
            AltText = "altText",
            Main_Base64 = "main-base",
            TargetType = MediaTargetType.Category_Icon,
            Thumb_Base64 = "thumb-base64",
            Title = "title",
            SortingOrder = 1,
            ThumbnailUniqueName = "thumbnail-unique-name",
            Type = MediaType.Image,

        };
    }

    public MediaBuilder WithUniqueName( string uniqueName)
    {
        _media.UniqueName = uniqueName;
        return this;
    }
   
    public MediaBuilder WithUrl(string url)
    {
        _media.URL = url;
        return this;
    }

    public MediaBuilder WithExtension(string extension)
    {
        _media.Extension = extension;
        return this;
    }

    public MediaBuilder WithHash(string hash)
    {
        _media.Hash = hash;
        return this;
    }

    public MediaBuilder WithTitle(string title)
    {
        _media.Title = title;
        return this;
    }

    public MediaBuilder WithThumbnailUniqueName(string thumbNameUniqueName)
    {
        _media.ThumbnailUniqueName = thumbNameUniqueName;
        return this;
    }

    public MediaBuilder WithAltText(string alteText)
    {
        _media.AltText= alteText;   
        return this;
    }

    public MediaBuilder WithSortingOrder(int sortingOredr)
    {
        _media.SortingOrder = sortingOredr; 
        return this;
    }

    public MediaBuilder WithMediaType(MediaType type)
    {
        _media.Type = type;
        return this;
    }

    public MediaBuilder WithMediaTargetType(MediaTargetType targerType)
    {
        _media.TargetType = targerType;
        return this;    
    }

    public MediaBuilder WithUserId(long useId)
    {
        _media.UserId = useId;
        return this;
    }

    public MediaBuilder WithMainBase64(string base64)
    {
        _media.Main_Base64 = base64;
        return this;
    }

    public MediaBuilder WithThumbBase64(string base64)
    {
        _media.Thumb_Base64 = base64;
        return this;
    }

    public Media Build()
    {
        return _media;
    }
}

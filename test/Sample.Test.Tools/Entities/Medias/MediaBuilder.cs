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


    public MediaBuilder WithUserId(long useId)
    {
        _media.UserId = useId;
        return this;
    }

    public Media Build()
    {
        return _media;
    }
}

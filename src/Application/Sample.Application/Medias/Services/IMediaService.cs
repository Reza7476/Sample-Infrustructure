using Sample.Application.Medias.Dtos;
using Sample.Commons.Interfaces;
using Sample.Core.Entities.Medias;

namespace Sample.Application.Medias.Services;

public interface IMediaService : IScope
{
    Task<Media> CreateMediaModelFromFile(CreateMediaDto dto);
    Task DeleteFileInHost(MediaType type, MediaTargetType targetType, string fileName);
    Task SaveFileInHostFromBase64(Media media);
}

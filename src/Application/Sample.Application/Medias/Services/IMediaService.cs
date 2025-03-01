using Sample.Application.Medias.Dtos;
using Sample.Commons.Interfaces;
using Sample.Core.Entities.Media;

namespace Sample.Application.Medias.Services;

public interface IMediaService : ITransient
{
    Task<Media> CreateMediaModelFromFile(CreateMediaDto dto);

}

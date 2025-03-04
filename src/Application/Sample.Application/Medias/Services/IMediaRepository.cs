using Sample.Commons.Interfaces;
using Sample.Core.Entities.Medias;

namespace Sample.Application.Medias.Services;

public interface IMediaRepository : IBaseRepository<Media>, IRepository
{
    Task Add(Media image);
    Task <List<Media>>GetAll();
}

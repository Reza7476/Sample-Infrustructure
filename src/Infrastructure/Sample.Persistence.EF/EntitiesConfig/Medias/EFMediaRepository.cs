using Sample.Application.Medias.Services;
using Sample.Core.Entities.Medias;
using Sample.Persistence.EF.DbContexts;

namespace Sample.Persistence.EF.EntitiesConfig.Medias;

public class EFMediaRepository : BaseRepository<Media>, IMediaRepository
{
    public EFMediaRepository(EFDataContext context) : base(context)
    {
    }




}

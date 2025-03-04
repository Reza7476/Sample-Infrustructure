using Microsoft.EntityFrameworkCore;
using Sample.Application.Medias.Services;
using Sample.Core.Entities.Medias;
using Sample.Persistence.EF.DbContexts;

namespace Sample.Persistence.EF.EntitiesConfig.Medias;

public class EFMediaRepository : BaseRepository<Media>, IMediaRepository
{

    private readonly DbSet<Media> _medias;
    public EFMediaRepository(EFDataContext context) : base(context)
    {
        _medias=context.Set<Media>();    
    }

    public async Task Add(Media image)
    {
        await _medias.AddAsync(image);
    }

    public async Task<List<Media>> GetAll()
    {
        return await _medias.ToListAsync();
    }
}

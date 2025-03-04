using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sample.Core.Entities.Medias;

namespace Sample.Persistence.EF.EntitiesConfig.Medias;

public class MediaConfiguration : IEntityTypeConfiguration<Media>
{
    public void Configure(EntityTypeBuilder<Media> builder)
    {

        builder.ToTable("Medias").HasKey(x => x.Id);

        builder.Property(_ => _.Id).IsRequired().ValueGeneratedOnAdd();

        builder.Property(_ => _.UniqueName).IsRequired();
        
        builder.Property(_ => _.URL).IsRequired();
        
        builder.Property(_ => _.Extension).IsRequired();
        
        builder.Property(_ => _.Hash).IsRequired();
        
        builder.Property(_ => _.Title).IsRequired(false);
        
        builder.Property(_ => _.ThumbnailUniqueName).IsRequired(false);
        
        builder.Property(_ => _.AltText).IsRequired(false);
        
        builder.Property(_ => _.SortingOrder).IsRequired();
        
        builder.Property(_ => _.Type).IsRequired();
        
        builder.Property(_ => _.TargetType).IsRequired();
        
        builder.Property(_ => _.UserId).IsRequired(false);
        
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sample.Core.Entities.Generals;

namespace Sample.Persistence.EF.EntitiesConfig;


internal sealed class BaseEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
    where TEntity : BaseEntity
{
    public void Configure(EntityTypeBuilder<TEntity> _)
    {
        _.HasKey(x => x.Id);

        _.Property(e => e.CreatedAt)
            .HasColumnType("bigint").IsRequired();

        _.Property(e => e.UpdatedAt)
            .HasColumnType("bigint").IsRequired(false);

        _.Property(e => e.CreatedBy)
            .HasMaxLength(36).IsRequired(false);

        _.Property(e => e.UpdatedBy)
            .HasMaxLength(36).IsRequired(false);
    }
}
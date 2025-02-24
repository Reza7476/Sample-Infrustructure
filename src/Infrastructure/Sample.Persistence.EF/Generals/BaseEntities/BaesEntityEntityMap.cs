using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sample.Core.Entities.Generals;

namespace Sample.Persistence.EF.Generals.BaseEntities;

public class BaesEntityEntityMap : IEntityTypeConfiguration<BaseEntity>
{
    public void Configure(EntityTypeBuilder<BaseEntity> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(_=>_.Id).IsRequired().ValueGeneratedOnAdd();

        builder.Property(e => e.CreatedAt)
            .HasColumnType("bigint").IsRequired();

        builder.Property(e => e.UpdatedAt)
            .HasColumnType("bigint").IsRequired(false);

        builder.Property(e => e.CreatedBy)
            .HasMaxLength(36).IsRequired(false);

        builder.Property(e => e.UpdatedBy)
            .HasMaxLength(36).IsRequired(false);
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sample.Core.Entities.Users;

namespace Sample.Persistence.EF.EntitiesConfig.Users;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasMany(_ => _.Medias)
            .WithOne(_ => _.User)
            .HasForeignKey(_=>_.UserId)
            .IsRequired(false);

        builder.Property(_ => _.FirstName);
        builder.Property(_ => _.Email);
    }
}
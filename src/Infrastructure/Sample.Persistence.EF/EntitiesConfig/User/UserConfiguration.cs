using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sample.Persistence.EF.EntitiesConfig.User;

public class UserConfiguration : IEntityTypeConfiguration<Sample.Core.Entities.Users.User>
{
    public void Configure(EntityTypeBuilder<Core.Entities.Users.User> builder)
    {
        builder.Property(_ => _.Name);
        builder.Property(_ => _.Email);
    }
}
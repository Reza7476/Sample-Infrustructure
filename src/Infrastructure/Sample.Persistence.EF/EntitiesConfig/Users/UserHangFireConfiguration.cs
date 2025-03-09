using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sample.Core.Entities.Users;

namespace Sample.Persistence.EF.EntitiesConfig.Users;

public class UserHangFireConfiguration : IEntityTypeConfiguration<UserHangfire>
{
    public void Configure(EntityTypeBuilder<UserHangfire> builder)
    {
        builder.ToTable("UserHangfires");
        builder.Property(_ => _.Status).IsRequired();
        builder.Property(_ => _.UserId).IsRequired();
    }
}

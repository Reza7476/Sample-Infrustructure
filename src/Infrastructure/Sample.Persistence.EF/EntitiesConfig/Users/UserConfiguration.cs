using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sample.Core.Entities.Users;

namespace Sample.Persistence.EF.EntitiesConfig.Users;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.Property(_ => _.MacId).IsRequired(false);

        builder.Property(_ => _.FirstName);
        
        builder.Property(_ => _.LastName);
        
        builder.Property(_ => _.Email);
        
        builder.Property(_ => _.Mobile);
        
        builder.Property(_ => _.ProfileUrl);
        
        builder.Property(_ => _.Gender);
        
        builder.Property(_ => _.NationalCode);
     
        builder.HasMany(_ => _.Medias)
            .WithOne(_ => _.User)
            .HasForeignKey(_=>_.UserId)
            .IsRequired(false);
    }
}
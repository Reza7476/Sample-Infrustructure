﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sample.Core.Entities.Users;

namespace Sample.Persistence.EF.Repositories.Users;

public class UserEntityMap : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> _)
    {
        _.ToTable("Users");

        _.Property(_ => _.Name).IsRequired();

        _.Property(_ => _.Name);
    }
}

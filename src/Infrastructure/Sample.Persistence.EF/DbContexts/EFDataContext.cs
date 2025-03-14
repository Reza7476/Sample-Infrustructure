﻿using Microsoft.EntityFrameworkCore;
using Sample.Core.Entities.Generals;
using Sample.Persistence.EF.EntitiesConfig;

namespace Sample.Persistence.EF.DbContexts;

public class EFDataContext : DbContext
{
    public EFDataContext(DbContextOptions<EFDataContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(EFDataContext).Assembly);
        base.OnModelCreating(modelBuilder);
        modelBuilder.Ignore<BaseEntity>();

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType) && entityType.ClrType != typeof(BaseEntity))
            {
                var entityMethod = typeof(ModelBuilder)
                    .GetMethod(nameof(ModelBuilder.Entity), new Type[] { })
                    ?.MakeGenericMethod(entityType.ClrType);

                if (entityMethod == null)
                {
                    continue;
                }

                var entityBuilder = entityMethod.Invoke(modelBuilder, null);

                var applyMethod = typeof(BaseEntityConfiguration<>)
                    .MakeGenericType(entityType.ClrType)
                    .GetMethod(nameof(BaseEntityConfiguration<BaseEntity>.Configure));

                if (applyMethod != null)
                {
                    applyMethod.Invoke(Activator.CreateInstance(typeof(BaseEntityConfiguration<>).MakeGenericType(entityType.ClrType)), new[] { entityBuilder });
                }
            }
        }
    }
}

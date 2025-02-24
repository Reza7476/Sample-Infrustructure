using Microsoft.EntityFrameworkCore;
using Sample.Core.Entities.Employees;
using Sample.Core.Entities.Generals;
using Sample.Core.Entities.Users;

namespace Sample.Persistence.EF.Persistence;

public class EFDataContext : DbContext
{
    public EFDataContext(DbContextOptions<EFDataContext> options)
        : base(options)
    {
    }

    //public EFDataContext(string connectionString)
    //    : this(new DbContextOptionsBuilder<EFDataContext>()
    //         .UseSqlServer(connectionString).Options)
    //{

    //}

    public DbSet<User> Users { get; set; }
    public DbSet<Employee> Employees { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(EFDataContext).Assembly);
        base.OnModelCreating(modelBuilder);

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
            {
                var entityMethod = typeof(ModelBuilder)
                    .GetMethod(nameof(ModelBuilder.Entity), new Type[] { })?
                    .MakeGenericMethod(entityType.ClrType);
                if (entityMethod == null) continue;

                var entityBuilder = entityMethod.Invoke(modelBuilder, null);

            }

            }

        }
}

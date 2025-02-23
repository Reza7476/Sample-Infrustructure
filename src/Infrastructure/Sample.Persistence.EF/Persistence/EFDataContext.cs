using Microsoft.EntityFrameworkCore;

namespace Sample.Persistence.EF.Persistence;

public class EFDataContext : DbContext
{
    public EFDataContext(string connectionString)
        : this(new DbContextOptionsBuilder<EFDataContext>()
             .UseSqlServer(connectionString).Options)
    {

    }

    public EFDataContext(DbContextOptions<EFDataContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(EFDataContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}

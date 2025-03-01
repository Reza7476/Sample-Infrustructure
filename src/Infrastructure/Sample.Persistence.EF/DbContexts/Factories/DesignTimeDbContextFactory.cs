using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Sample.Persistence.EF.DbContexts.Factories;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<EFDataContext>
{
    public EFDataContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()          //ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())    // Adjust path if needed
               .AddJsonFile("infrastructureAppSettings.json")   // Read connection string from config
               .Build();

        var optionsBuilder = new DbContextOptionsBuilder<EFDataContext>();
        var connectionString = configuration.GetConnectionString("SqlServerDevelopment");

        optionsBuilder.UseSqlServer(connectionString);

        return new EFDataContext(optionsBuilder.Options);
    }
}

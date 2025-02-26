using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sample.Persistence.EF.DbContexts;

namespace Sample.Persistence.EF.Extensions;

public static class DependencyInjection
{

    public static IServiceCollection SetDBContext(this IServiceCollection services, IConfiguration configuration)
    {
        string? databaseType = configuration.GetValue<string>("DatabaseType");
        string? connectionString = configuration.GetConnectionString(databaseType ?? "Undeclared");

        switch (databaseType)
        {
            case "SqlServerProduction":
                services.AddDbContext<EFDataContext>((serviceProvider, options) =>
                {
                    // Configure DbContext using IConfiguration
                    options.UseSqlServer(connectionString,
                        builder => builder.MigrationsAssembly(typeof(EFDataContext).Assembly.FullName));
                });

                break;
            case "SqlServerDevelopment":
                services.AddDbContext<EFDataContext>((serviceProvider, options) =>
                {
                    // Configure DbContext using IConfiguration
                    options.UseSqlServer(connectionString,
                        builder => builder.MigrationsAssembly(typeof(EFDataContext).Assembly.FullName));
                });

                break;
            
            default:
                throw new InvalidOperationException($"Unsupported database type: {databaseType}");
        }
        return services;
    }

}

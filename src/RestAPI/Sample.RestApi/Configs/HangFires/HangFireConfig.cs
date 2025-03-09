using Hangfire;
using Hangfire.Dashboard.BasicAuthorization;
using Hangfire.SqlServer;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SixLabors.ImageSharp;

namespace Sample.RestApi.Configs.HangFires;

public static class HangFireConfig
{
    public static void ConfigureHangfireService(this IServiceCollection services, IConfiguration configuration)
    {
        string? connectionString = configuration.GetConnectionString("SqlServerDevelopment");
        if (connectionString != null)
        {
            services.AddHangfire(config =>
            {
                config.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                      .UseSimpleAssemblyNameTypeSerializer()
                      .UseRecommendedSerializerSettings()
                      .UseSqlServerStorage(connectionString, new SqlServerStorageOptions
                      {
                          CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                          SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                          QueuePollInterval = TimeSpan.Zero,
                          UseRecommendedIsolationLevel = true,
                          DisableGlobalLocks = true
                      });
            });

            services.AddHangfireServer();
        }
    }

    public static IApplicationBuilder UseAppHangfire(
        this IApplicationBuilder app,
        IConfiguration config)
    {
        app.UseHangfireDashboard("/hangfire", new DashboardOptions
        {
            Authorization = new[]
            {
                 new BasicAuthAuthorizationFilter(new BasicAuthAuthorizationFilterOptions
                 {
                     RequireSsl=false,
                     SslRedirect=false,
                     LoginCaseSensitive=true,
                     Users= new []
                     {
                         new BasicAuthAuthorizationUser
                         {
                             Login="admin",
                             PasswordClear="admin"
                         }
                     }
                 })
            },
            DisplayNameFunc = (_, job) =>
            {
                var argumentTypes =
                    string.Join(
                        ", ",
                        job.Args.Select(arg => arg?.GetType().Name));

                var className = job.Method.DeclaringType?.Name;
                return $"{className}.{job.Method.Name}: {argumentTypes}";
            }
        });
        return app;
    }


  


}

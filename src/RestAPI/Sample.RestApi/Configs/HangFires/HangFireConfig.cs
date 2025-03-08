using Hangfire;
using Hangfire.SqlServer;

namespace Sample.RestApi.Configs.HangFires;

public static class HangFireConfig
{

    public static void ConfigureHangfire(this IServiceCollection services,
        IConfiguration configuration)
    {

        string connectionstring = configuration.GetConnectionString("SqlServerDevelopment");

        services.AddHangfire(config =>
        {
            config.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                  .UseSimpleAssemblyNameTypeSerializer()
                  .UseRecommendedSerializerSettings()
                  .UseSqlServerStorage(connectionstring, new SqlServerStorageOptions
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

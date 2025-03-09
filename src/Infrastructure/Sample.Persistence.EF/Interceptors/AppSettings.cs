using Microsoft.Extensions.Configuration;
using Sample.Persistence.EF.Interfaces;

namespace Sample.Persistence.EF.Interceptors;

internal class AppSettings : IInfraastructureAppService
{
    public string? ConnectionString  {get;}


    public AppSettings(IConfiguration configuration)
    {
        ConnectionString = configuration.GetConnectionString("SqlServerDevelopment");
    }
}

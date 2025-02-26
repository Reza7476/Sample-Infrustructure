using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sample.Persistence.EF.Extensions;

namespace Sample.Persistence.EF;

public static class ConfigurationService
{

    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection service,
        IConfiguration configuration)
    {

        service.SetDBContext(configuration);

        return service;
    }
}

using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using Sample.Application.Users;
using Sample.Commons.Interfaces;
using Sample.Persistence.EF.Persistence;
using Sample.Persistence.EF.Repositories.Users;

namespace Sample.RestApi.Configs.Services;

public static class ConfigAutofac
{
    public static ConfigureHostBuilder AddAutoFact(this ConfigureHostBuilder builder)
    {
        builder.UseServiceProviderFactory(new AutofacServiceProviderFactory());
        builder.ConfigureContainer<ContainerBuilder>(builder =>
        {
            builder.RegisterModule(new AutofactModule());

        });

        return builder;
    }
}

public class AutofactModule : Module
{
    protected override void Load(ContainerBuilder container)
    {
        var serviceAssembely = typeof(UserAppService).Assembly;
        var persistenceAssembley = typeof(EFUnitOfWork).Assembly;
        var repositoryAssembley = typeof(EFUserRepository).Assembly;

        container.RegisterAssemblyTypes(persistenceAssembley)
         .AssignableTo<IScope>()
         .AsImplementedInterfaces()
         .InstancePerLifetimeScope();//==>add scoped

        container.RegisterAssemblyTypes(persistenceAssembley)
         .AssignableTo<IRepository>()
         .AsImplementedInterfaces()
         .InstancePerLifetimeScope();
        //.SingleInstance(); ===>AddSingleton()

        container.RegisterAssemblyTypes(serviceAssembely)
         .AssignableTo<Service>()
         .AsImplementedInterfaces()
         .InstancePerLifetimeScope();
        //.InstancePerDependency();===>AddTransient()

        container.Register(ctx =>
        {
            var clientFactory = ctx.Resolve<IHttpClientFactory>();
            return clientFactory.CreateClient();
        }).As<HttpClient>().InstancePerLifetimeScope();

        base.Load(container);
    }
}
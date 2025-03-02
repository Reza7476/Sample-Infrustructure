using Autofac;
using Autofac.Extensions.DependencyInjection;
using Sample.Application.Users;
using Sample.Commons.Interfaces;
using Sample.Persistence.EF.DbContexts;

namespace Sample.RestApi.Configs.Services;

public static class ConfigAutofac
{
    public static ConfigureHostBuilder AddAutoFact(this ConfigureHostBuilder builder)
    {
        builder.UseServiceProviderFactory(new AutofacServiceProviderFactory());
        builder.ConfigureContainer<ContainerBuilder>(builder =>
        {
            builder.RegisterModule(new AutoFacModule());
        });

        return builder;
    }
}

public class AutoFacModule : Module
{
    protected override void Load(ContainerBuilder container)
    {
        var serviceAssembly = typeof(UserAppService).Assembly;
        var persistenceAssembly = typeof(EFUnitOfWork).Assembly;
        var restApiAssembly = typeof(AutoFacModule).Assembly;

        // ثبت تمامی انواعی که از IScope ارث می‌برند
        container.RegisterAssemblyTypes(
            persistenceAssembly,
            serviceAssembly,
            restApiAssembly)
         .Where(t => typeof(IScope).IsAssignableFrom(t))  // اضافه کردن شرط برای ارث‌بری از IScope
         .AsImplementedInterfaces()
         .InstancePerLifetimeScope();  // Scoped

        // ثبت IRepository‌ها با زمان‌زندگی Scoped
        container.RegisterAssemblyTypes(persistenceAssembly)
         .AssignableTo<IRepository>()
         .AsImplementedInterfaces()
         .InstancePerLifetimeScope();

        // ثبت IService‌ها با زمان‌زندگی Scoped
        container.RegisterAssemblyTypes(serviceAssembly)
         .AssignableTo<IService>()
         .AsImplementedInterfaces()
         .InstancePerLifetimeScope();

        // ثبت HttpClient با زمان‌زندگی Scoped
        container.Register(ctx =>
        {
            var clientFactory = ctx.Resolve<IHttpClientFactory>();
            return clientFactory.CreateClient();
        }).As<HttpClient>().InstancePerLifetimeScope();

        base.Load(container);
    }
}

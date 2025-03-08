using Hangfire;
using Sample.Application.Users.Services;

namespace Sample.RestApi.Configs.RequrringJobs;

public static class SetUp
{
    public static IApplicationBuilder SetUpRequrringJob(this IApplicationBuilder app)
    {
        var jobManager = app.ApplicationServices.GetRequiredService<IRecurringJobManager>();

        jobManager.RemoveIfExists("SetUserStatusJob");

        jobManager
           .AddOrUpdate<IUserService>(
                    "Set User Status",
                    myJob => myJob.SetUserStatus(),
                  Cron.Daily());

        return app; 
    }
}

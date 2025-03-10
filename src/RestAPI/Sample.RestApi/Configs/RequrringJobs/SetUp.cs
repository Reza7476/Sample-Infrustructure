using Dapper;
using Hangfire;
using Microsoft.Data.SqlClient;
using Sample.Application.Users.Services;
using Sample.Persistence.EF.Interfaces;

namespace Sample.RestApi.Configs.RequrringJobs;

public static class SetUp
{
    public static IApplicationBuilder SetUpRequrringJob(this IApplicationBuilder app)
    {
        var jobManager = app.ApplicationServices.GetRequiredService<IRecurringJobManager>();
        var appSettings = app.ApplicationServices.GetRequiredService<IInfrastructureAppService>();
        string connectionString = appSettings.ConnectionString!;

        var jobIds = new List<string> {
            "SetUserStatusJob",
            "CleanupOldJobs"
        };

        foreach (var jobId in jobIds)
        {
            jobManager.RemoveIfExists(jobId);
        }

        jobManager
               .AddOrUpdate<IUserService>(
               "SetUserStatusJob",
               myJob => myJob.SetUserStatus(),
               Cron.Hourly(5));

        jobManager
               .AddOrUpdate(
               "CleanupOldJobs",
               () => CleanupOldJobs(connectionString!),
               Cron.Daily());

        return app;
    }

    public static void CleanupOldJobs(string connectionString)
    {
        using (var connection = new SqlConnection(connectionString))
        {
            connection.Open();

            // حذف رکوردهای قدیمی از جدول Job
            var deleteJobsQuery = "DELETE FROM Hangfire.Job WHERE StateName = 'Succeeded' AND CreatedAt < @ThresholdDate";
            var deleteStateQuery = "DELETE FROM Hangfire.State WHERE Name = 'Succeeded' AND CreatedAt < @ThresholdDate";

            // تعریف تاریخ آستانه (مثلا 1 روز گذشته)
            var thresholdDate = DateTime.UtcNow.AddDays(-1); // 1 روز گذشته

            // حذف رکوردهای قدیمی از جدول Job
            connection.Execute(deleteJobsQuery, new { ThresholdDate = thresholdDate });

            // حذف رکوردهای قدیمی از جدول State
            connection.Execute(deleteStateQuery, new { ThresholdDate = thresholdDate });
        }
    }

}

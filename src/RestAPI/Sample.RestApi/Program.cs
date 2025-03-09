using Hangfire;
using Hangfire.Dashboard;
using Hangfire.SqlServer;
using Sample.Persistence.EF;
using Sample.RestApi;
using Sample.RestApi.Configs.Cors;
using Sample.RestApi.Configs.HangFires;
using Sample.RestApi.Configs.Middleware;
using Sample.RestApi.Configs.RequrringJobs;
using Sample.RestApi.Configs.Services;
using Sample.RestApi.Configs.Swagger;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddHttpClient();

builder.Host.AddAutoFact();

builder.Services.AddAuthentication();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddCorsConfiguration();

builder.Services.AddSwagger();

var isDev = builder.Environment.IsDevelopment();

string? infrastructureDirectory;
if (isDev)
{
    infrastructureDirectory = InfrastructureHelper.GetInfrastructureDirectory();
}
else
{
    infrastructureDirectory = Directory.GetCurrentDirectory();
}

var settings = builder.Configuration
    .SetBasePath(infrastructureDirectory!)
    .AddJsonFile("infrastructureAppSettings.json", optional: false, reloadOnChange: true)
    .Build();

builder.Services.ConfigureHangfireService(settings);
builder.Services.AddInfrastructureServices(settings);
builder.Services.AddPresentationService(settings);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();

app.UseAppHangfire(settings);

app.SetUpRequrringJob();

app.UseRouting();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseMiddleware<CheckUserInfo>();

app.MapControllers();

app.Run();


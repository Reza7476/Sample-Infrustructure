using Microsoft.EntityFrameworkCore;
using Sample.Persistence.EF.Persistence;
using Sample.RestApi.Configs.Cors;
using Sample.RestApi.Configs.Middlewares;
using Sample.RestApi.Configs.Services;
using Sample.RestApi.Configs.Swagger;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<EFDataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddHttpClient();

builder.Host.AddAutoFact();

builder.Services.AddAuthentication();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddCorsConfiguration();

builder.Services.AddSwagger();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<TokenValidationMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

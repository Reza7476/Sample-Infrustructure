namespace Sample.RestApi.Configs.Cors;

public static class CorsConfiguration
{
    public static void AddCorsConfiguration(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", policy =>
            {
                policy.AllowAnyOrigin()  // اجازه به هر اوریجن (Domain)
                      .AllowAnyMethod()  // اجازه به هر متد (GET, POST, PUT, DELETE)
                      .AllowAnyHeader(); // اجازه به هر هدر
            });
        });
    }

    public static void UseCorsConfiguration(this IApplicationBuilder app)
    {
        app.UseCors("AllowAll");
    }
}


using Microsoft.OpenApi.Models;

namespace Sample.RestApi.Configs.Swagger;

public static class SwaggerConfiguration
{
    public static void AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            // تعریف security scheme برای Bearer token
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter your token",
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey
            });

            // اضافه کردن security requirement به Swagger
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                }
            });
        });
    }


}

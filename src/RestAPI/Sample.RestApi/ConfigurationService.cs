using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.IdentityModel.Tokens;

namespace Sample.RestApi;

public static class ConfigurationService
{
    public static IServiceCollection AddPresentationService(
     this IServiceCollection service,
     IConfiguration configuration)
    {
        var issuer = configuration["Authentication:issuer"];
        var audience = configuration["Authentication:audience"];
        service.AddHttpContextAccessor();
        service.AddMvc()
        .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
        .AddDataAnnotationsLocalization();

        service.AddSingleton<ITempDataDictionaryFactory, TempDataDictionaryFactory>();

        service.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(option =>
            {
                option.Authority = issuer;
                option.Audience = audience;
                option.RequireHttpsMetadata = true;
                option.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = issuer, // OpenIddict issuer
                    ValidAudience = audience, // API audience
                    NameClaimType = "name",
                    RoleClaimType = "role",
                };
            }).AddCookie();

        service
            .AddControllersWithViews()
            .AddRazorRuntimeCompilation();


        return service;
    }
}

using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using Sample.Application.Interfaces;
using Sample.Application.Users.Dtos;
using Sample.Application.Users.Services;
using Sample.Commons.Interfaces;
using System.Net.Http.Headers;

namespace Sample.RestApi.Configs.Middleware;

public class CheckUserInfo
{
    private readonly RequestDelegate _next;
    private readonly IConfiguration _configuration;
    private const string UserInfoUrl = "/connect/userinfo";
    private static readonly SemaphoreSlim semaphore = new SemaphoreSlim(1, 1); // Allow only one thread to proceed at a time.

    public CheckUserInfo(RequestDelegate next,
        IConfiguration configuration)
    {
        _next = next;
        _configuration = configuration;
    }

    public async Task InvokeAsync(HttpContext context, IServiceProvider serviceProvider)
    {
        var userInfoUrl = _configuration["Authentication:issuer"];
        var userTokenService = serviceProvider.GetRequiredService<IUserTokenService>();
        var unitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>();
        var userRepository = serviceProvider.GetRequiredService<IUserRepository>();
        var userService = serviceProvider.GetRequiredService<IUserService>();
        var token = context.Request.Headers["Authorization"].ToString()?.Replace("Bearer ", "");
        var endpoint = context.GetEndpoint();
        var hasAllowAnonymousAttribute = endpoint?.Metadata
           .GetMetadata<AllowAnonymousAttribute>() != null;

        if (hasAllowAnonymousAttribute)
        {
            await _next(context);
            return;
        }

        var hasAuthorize = endpoint?.Metadata.GetMetadata<AuthorizeAttribute>() != null;
        if (!hasAuthorize)
        {
            await _next(context);
            return;
        }

        if (string.IsNullOrEmpty(token))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("Authorization token is missing.");
            return;
        }

        try
        {
            var mac_Id = userTokenService.Mac_Id;
            if (string.IsNullOrEmpty(mac_Id))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Invalid token.");
                return;
            }
            else
            {
                var userId = await userRepository.GetUserIdByMacId(mac_Id);

                if (userId == 0)
                {
                    // Acquire the semaphore lock to ensure only one request creates the user
                    await semaphore.WaitAsync();

                    try
                    {
                        userId = await userRepository.GetUserIdByMacId(mac_Id);// Check again to avoid creating the same user if another request already created it

                        if (userId == 0)
                        {
                            var accessToken = context.Request.Headers["Authorization"].ToString();
                            if (accessToken.Contains("Bearer"))
                            {
                                accessToken = accessToken.Substring("Bearer ".Length).Trim();
                            }
                            var client = new HttpClient();
                            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                            var response = await client.GetAsync(userInfoUrl + UserInfoUrl);
                            // Read and parse the JSON response
                            var content = await response.Content.ReadAsStringAsync();
                            var userModel = JsonConvert.DeserializeObject<UserInfoResponseDto>(content);
                            if (userModel == null)
                            {
                                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                                await context.Response.WriteAsync("Invalid token.");
                                return;
                            }
                            else
                            {
                                userId = await userService.CreateAsync(userModel);
                            }
                        }
                    }
                    finally
                    {
                        // Release the semaphore so other requests can proceed
                        semaphore.Release();
                    }
                }

                context.Items["UserId"] = userId;
            }

            await _next(context);
        }
        catch (Exception)
        {
            throw;
        }

    }
}
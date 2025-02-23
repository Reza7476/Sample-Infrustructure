using System.Net.Http.Headers;

namespace Sample.RestApi.Configs.Middlewares;

public class TokenValidationMiddleware
{
    private readonly RequestDelegate _next;

    public TokenValidationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var authorizationHeader = context.Request.Headers["Authorization"].ToString();

        if (!string.IsNullOrEmpty(authorizationHeader) && authorizationHeader.StartsWith("Bearer "))
        {
            var token = authorizationHeader.Substring("Bearer ".Length).Trim();

            using var client = new HttpClient();

            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:7283/connect/check-token");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    await _next(context);
                    return;
                }
            }
            catch
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Invalid token.");
                return;
            }
        }

        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        await context.Response.WriteAsync("Unauthorized: Bearer token is missing or invalid.");
    }
}


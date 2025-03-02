using Microsoft.AspNetCore.Http;
using Sample.Commons.Interfaces;
using System.Security.Claims;

namespace Sample.Persistence.EF.Identities.Services;

public class UserTokenService : IUserTokenService
{

    private readonly IHttpContextAccessor _accessor;
    public UserTokenService(
        IHttpContextAccessor accessor)
    {
        _accessor = accessor;
    }

    public string? Mac_Id => GetUserFromToken();

    public int? UserId => GetUserIdFromHttpContent();
    private int? GetUserIdFromHttpContent()
    {
        if (string.IsNullOrWhiteSpace(_accessor.HttpContext?.Items["UserId"]?.ToString()))
            return null;

        return int.Parse(_accessor.HttpContext.Items["UserId"]?.ToString()!);
    }

    private string? GetUserFromToken()
    {
        return _accessor.HttpContext?.User.Claims
             .FirstOrDefault(_ => _.Type == ClaimTypes.NameIdentifier)?.Value;
    }
}

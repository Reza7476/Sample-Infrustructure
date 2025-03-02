using Sample.Commons.Interfaces;
using System.Security.Claims;

namespace Sample.RestApi.Configs.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public long? UserId
    {
        get
        {
            if (string.IsNullOrWhiteSpace(_httpContextAccessor.HttpContext?.Items["UserId"]?.ToString()))
                return null;

            return (long)_httpContextAccessor.HttpContext.Items["UserId"]!;
        }
    }

    public int GetCurrentUserId()
    {
        var loginUserContext = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Name);
        if (loginUserContext == null)
            throw new ArgumentNullException("You should pass Authorization-Token");

        int loginUserId = Convert.ToInt32(loginUserContext.Value);
        if (loginUserId == 0)
            throw new ArgumentNullException("You should pass Authorization-Token");

        return loginUserId;
    }
}
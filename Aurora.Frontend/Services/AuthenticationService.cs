using System.Security.Claims;

namespace Aurora.Frontend.Services;

public class AuthenticationService
{
    private readonly HttpContextAccessor _contextAccessor;

    public AuthenticationService(HttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }
    public ClaimsPrincipal? CurrentUser => _contextAccessor.HttpContext?.User;
}
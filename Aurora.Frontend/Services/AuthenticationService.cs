using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Aurora.Interfaces.Models;

namespace Aurora.Frontend.Services;

public class AuthenticationService
{
    public event Action<ClaimsPrincipal>? UserChanged;
    private ClaimsPrincipal? _currentUser;
    
    public ClaimsPrincipal CurrentUser
    {
        get => _currentUser ?? new();
        set
        {
            if (Equals(_currentUser, value)) return;

            _currentUser = value;
            if (UserChanged is not null)
            {
                UserChanged.Invoke(_currentUser);
            }
                    
        }
    }

    public ClaimsIdentity GetClaimsIdentity(AuroraUser user, string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(token);

        var identity = new ClaimsIdentity(jwtToken.Claims, "Custom Authentication");

        return identity;
    }
}
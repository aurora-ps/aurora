using Aurora.Interfaces.Models;

namespace Aurora.Frontend.Services
{
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components.Authorization;

    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private AuthenticationState authenticationState;

        public CustomAuthenticationStateProvider(AuthenticationService service)
        {
            authenticationState = new AuthenticationState(service.CurrentUser);

            service.UserChanged += OnUserChanged;
        }

        private void OnUserChanged(ClaimsPrincipal newUser)
        {
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(newUser)));
        }

        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            //var identity = new ClaimsIdentity(new[]
            //{
            //    new Claim(ClaimTypes.Name, "scott@wilkos.net"),
            //    new Claim(ClaimTypes.NameIdentifier, "0317fa18-52f9-4334-8f11-de08edf63ae8"),
            //}, "Custom Authentication");

            var identity = new ClaimsIdentity();
            var user = new ClaimsPrincipal(identity);

            return Task.FromResult(new AuthenticationState(user));
        }

        public async Task AuthenticateUser(AuroraUser user)
        {
            var identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
            }, "Custom Authentication");

            var claimsPrincipal = new ClaimsPrincipal(identity);
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }
    }

    public class AuthenticationService
    {
        public event Action<ClaimsPrincipal>? UserChanged;
        private ClaimsPrincipal? _currentUser;

        public AuthenticationService()
        {
        }

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
            var identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
            }, "Custom Authentication");

            return identity;
        }
    }
}

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
        }

        private void OnUserChanged(ClaimsPrincipal newUser)
        {
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(newUser)));
        }

        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var identity = new ClaimsIdentity();
            var user = new ClaimsPrincipal(identity);

            return Task.FromResult(new AuthenticationState(user));
        }

        public async Task AuthenticateUser(ClaimsIdentity identity)
        {
            var claimsPrincipal = new ClaimsPrincipal(identity);
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }
    }
}

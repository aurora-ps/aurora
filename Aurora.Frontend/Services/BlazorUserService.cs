using Aurora.Interfaces.Models;
using Microsoft.AspNetCore.Identity;

namespace Aurora.Frontend.Services
{
    public class BlazorUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly SignInManager<AuroraUser> _signInManager;

        public BlazorUserService(IHttpContextAccessor httpContextAccessor, SignInManager<AuroraUser> signInManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _signInManager = signInManager;
        }

        public Task<bool> IsAuthenticatedAsync()
        {
            return Task.FromResult(_httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false);
        }

    }
}

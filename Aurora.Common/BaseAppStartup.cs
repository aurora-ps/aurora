using Microsoft.Extensions.DependencyInjection;

namespace Aurora.Common
{
    public abstract class BaseAppStartup
    {
        public abstract void Bootstrap(IServiceCollection services);
        public abstract int Priority { get; }
    }
}
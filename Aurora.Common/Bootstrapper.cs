using Microsoft.Extensions.DependencyInjection;

namespace Aurora.Common;

public static class Bootstrapper
{
    /// <summary>
    /// The _apps.
    /// </summary>
    private static IEnumerable<BaseAppStartup>? _apps;

    /// <summary>
    /// The apps.
    /// </summary>
    private static IEnumerable<BaseAppStartup> Apps =>
        _apps ?? (_apps = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(a => a.GetTypes())
            .Where(t => typeof(BaseAppStartup).IsAssignableFrom(t) && t.IsClass && !t.IsAbstract)
            .Select(t => (BaseAppStartup)Activator.CreateInstance(t))
            .OrderBy(s => s.Priority)
            .ToList());

    public static void BootstrapServices(this IServiceCollection services)
    {
        foreach (var app in Apps)
        {
            app.Bootstrap(services);
        }
    }
}
using Aurora.Common;
using Aurora.Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;

namespace Aurora.Infrastructure
{
    public class AppStartup : BaseAppStartup
    {
        public override void Bootstrap(IServiceCollection services)
        {
            services.AddScoped<IReportDbContext, ReportDbContext>();
            services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
        }

        public override int Priority { get; } = 10;
    }
}

using Microsoft.Extensions.Logging;

namespace Aurora.Web.Shared.Extensions;

public static class OrleansExtensions
{
    public static void AddOrleansSilo(this ISiloBuilder siloBuilder, int port, int gatewayPort,
        bool useDashboard = false)
    {
        siloBuilder.UseLocalhostClustering(port, gatewayPort);
        siloBuilder.AddMemoryGrainStorageAsDefault();
        siloBuilder.AddMemoryGrainStorage("auroraStorage");

        if (useDashboard)
            siloBuilder.UseDashboard();

        siloBuilder.ConfigureLogging(logging => logging.AddConsole().SetMinimumLevel(LogLevel.Warning));
    }
}
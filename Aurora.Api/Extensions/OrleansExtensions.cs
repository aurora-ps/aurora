namespace Aurora.Api.Extensions
{
    public static class OrleansExtensions
    {
        public static void AddOrleansSilo(this ISiloBuilder siloBuilder, int port, int gatewayPort, bool useDashboard = false)
        {
            siloBuilder.UseLocalhostClustering(siloPort: port, gatewayPort: gatewayPort);
            siloBuilder.AddMemoryGrainStorageAsDefault();
            siloBuilder.AddMemoryGrainStorage("auroraStorage");

            if (useDashboard)
                siloBuilder.UseDashboard();

            siloBuilder.ConfigureLogging(logging => logging.AddConsole().SetMinimumLevel(LogLevel.Warning));

        }
    }
}

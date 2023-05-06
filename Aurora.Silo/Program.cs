using Aurora.Data.Interfaces;
using Aurora.Grains.Services;
using Aurora.Interfaces;
using Aurora.Silo.Startup;

await CreateSilo(11111, true).StartAsync();

Console.WriteLine("Orleans is running.\nPress Enter to terminate...");
Console.ReadLine();
Console.WriteLine("Orleans is stopping...");

IHost CreateSilo(int port, bool useDashboard = false)
{
    void ConfigureOrleansForLocal(ISiloBuilder siloBuilder)
    {
        siloBuilder.UseLocalhostClustering(port);
        siloBuilder.AddMemoryGrainStorageAsDefault();
        siloBuilder.AddMemoryGrainStorage("auroraStorage");

        if (useDashboard)
            siloBuilder.UseDashboard();

        siloBuilder.ConfigureLogging(logging => logging.AddConsole().SetMinimumLevel(LogLevel.Warning));

        siloBuilder.AddStartupTask<BootstrapStartupTask>();
    }

    var builder = new HostBuilder();

    builder.ConfigureServices(services =>
    {
        services.AddScoped<IDataService<UserRecord, string>, UserDataService>();
        services.AddScoped<IOrganizationDataService, OrganizationDataService>();
    });

    builder.UseOrleans((context, siloBuilder) =>
    {
        if (context.HostingEnvironment.IsDevelopment())
            ConfigureOrleansForLocal(siloBuilder);
        else
            ConfigureOrleansForLocal(siloBuilder);
    });

    var host = builder.Build();
    return host;
}
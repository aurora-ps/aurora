using Aurora.Interfaces;

namespace Aurora.Api.Startup;

public class BootstrapStartupTask : IBootstrapStartupTask
{
    private readonly IGrainFactory _factory;
    private readonly ILogger _logger;

    public BootstrapStartupTask(ILoggerFactory loggerFactory, IGrainFactory factory)
    {
        _factory = factory;
        _logger = loggerFactory.CreateLogger<BootstrapStartupTask>();
    }

    public async Task Execute(CancellationToken cancellationToken)
    {
        _logger.LogInformation("BootstrapStartupTask.Execute called.");

        var organizationManagementGrain = _factory.GetGrain<IOrganizationManagementGrain>("");
        await organizationManagementGrain.AddAsync("Test Organization");

        var organizationGrain = _factory.GetGrain<IOrganizationGrain>("1");
        await organizationGrain.CreateAsync("Test Organization 1");

        await Task.CompletedTask;
    }
}
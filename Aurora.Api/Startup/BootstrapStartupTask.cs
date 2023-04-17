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

        var grain = _factory.GetGrain<IOrganizationGrain>("test");
        await grain.AddAsync("Test Organization");
        await Task.CompletedTask;
    }
}
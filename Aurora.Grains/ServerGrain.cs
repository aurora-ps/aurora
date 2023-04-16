using Aurora.Interfaces;
using Microsoft.Extensions.Logging;
using Orleans.Concurrency;
using Orleans.Runtime;

namespace Aurora.Grains;

[Reentrant]
public class ServerGrain : Grain, IServerGrain
{
    private readonly ILogger<ServerGrain> _logger;
    private readonly IPersistentState<ServerState> _state;

    public ServerGrain([PersistentState("server", "auroraStorage")] IPersistentState<ServerState> state,
        ILoggerFactory factory)
    {
        _state = state;
        _logger = factory.CreateLogger<ServerGrain>();
    }

    public Task<bool> IsInitialized()
    {
        return Task.FromResult(_state.State.IsInitialized);
    }

    public Task<ServerState> GetDetails()
    {
        return Task.FromResult(_state.State);
    }

    public Task AddOrganization(OrganizationRecord organization)
    {
        _state.State.Organizations.Add(organization);
        return _state.WriteStateAsync();
    }
}
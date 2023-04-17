using Aurora.Interfaces;
using Microsoft.Extensions.Logging;
using Orleans.Concurrency;
using Orleans.Runtime;

namespace Aurora.Grains;

public class ServerGrain : Grain, IServerGrain
{
    private readonly IPersistentState<Dictionary<string, OrganizationRecord>> _organizationState;

    private readonly ILogger<ServerGrain> _logger;

    public ServerGrain([PersistentState("server", "auroraStorage")] IPersistentState<Dictionary<string, OrganizationRecord>> organizationState,
        ILoggerFactory factory)
    {
        _organizationState = organizationState;
        _logger = factory.CreateLogger<ServerGrain>();
    }

    async Task IServerGrain.AddOrganizationToServer(OrganizationRecord organization)
    {
        _organizationState.State.Add(organization.Id, organization);

        await _organizationState.WriteStateAsync();
    }

    public Task<IList<OrganizationRecord>> GetOrganizations()
    {
        return Task.FromResult<IList<OrganizationRecord>>(_organizationState.State.Values.ToList());
    }
}
using Aurora.Interfaces;
using Microsoft.Extensions.Logging;
using Orleans.Concurrency;
using Orleans.Runtime;

namespace Aurora.Grains;

[Reentrant]
public class OrganizationGrain : Grain, IOrganizationGrain
{
    private readonly ILogger<OrganizationGrain> _logger;
    private readonly IPersistentState<OrganizationRecord> _state;

    public OrganizationGrain(
        [PersistentState("organization", "auroraStorage")]
        IPersistentState<OrganizationRecord> state,
        ILoggerFactory factory)
    {
        _state = state;
        _logger = factory.CreateLogger<OrganizationGrain>();
    }

    [ReadOnly]
    public Task<bool> IsInitialized()
    {
        return Task.FromResult(string.IsNullOrEmpty(_state.State?.Id));
    }

    [ReadOnly]
    public Task<OrganizationRecord?> GetDetailsAsync()
    {
        return Task.FromResult(string.IsNullOrEmpty(_state.State.Id) ? null : _state.State);
    }

    public Task<OrganizationRecord?> AddAsync(string name)
    {
        _state.State = new OrganizationRecord
        {
            Id = this.GetPrimaryKeyString(),
            Name = name
        };

        _state.WriteStateAsync();

        AddToServer(_state.State);

        return GetDetailsAsync();
    }

    private Task AddToServer(OrganizationRecord organization)
    {
        var serverGrain = GrainFactory.GetGrain<IServerGrain>("");
        return serverGrain.AddOrganization(organization);
    }
}
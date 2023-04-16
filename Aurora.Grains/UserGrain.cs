using Aurora.Interfaces;
using Microsoft.Extensions.Logging;
using Orleans.Concurrency;
using Orleans.Runtime;

namespace Aurora.Grains;

[Reentrant]
public class UserGrain : Grain, IUserGrain
{
    private readonly ILogger<UserGrain> _logger;
    private readonly IPersistentState<UserRecord> _state;

    public UserGrain(
        [PersistentState("user", "auroraStorage")]
        IPersistentState<UserRecord> state,
        ILoggerFactory factory)
    {
        _state = state;
        _logger = factory.CreateLogger<UserGrain>();
    }

    [ReadOnly]
    public Task<bool> IsInitialized()
    {
        return Task.FromResult(string.IsNullOrEmpty(_state.State?.Id));
    }

    [ReadOnly]
    public Task<UserRecord?> GetDetailsAsync()
    {
        return Task.FromResult(string.IsNullOrEmpty(_state.State.Id) ? null : _state.State);
    }

    public async Task<UserRecord?> AddAsync(string name, string email)
    {
        _state.State = new UserRecord
        {
            Id = this.GetPrimaryKeyString(),
            Name = name,
            Email = email
        };

        await _state.WriteStateAsync();

        var userService = GrainFactory.GetGrain<IUserServiceGrain>("");
        await userService.AddOrUpdateUserAsync(_state.State.Id, _state.State);
        return await GetDetailsAsync();
    }

    public async Task<bool> ExistsAsync(string userId)
    {
        return (await GetDetailsAsync() != null);
    }
}
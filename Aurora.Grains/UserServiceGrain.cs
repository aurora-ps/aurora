using Aurora.Interfaces;
using Microsoft.Extensions.Logging;
using Orleans.Concurrency;
using Orleans.Runtime;

namespace Aurora.Grains;

[Reentrant]
public class UserServiceGrain : Grain, IUserServiceGrain
{
    private readonly ILogger<UserServiceGrain> _logger;
    private readonly IPersistentState<UserServiceState> _state;

    public UserServiceGrain(
        [PersistentState("userService", "auroraStorage")]
        IPersistentState<UserServiceState> state,
        ILoggerFactory factory)
    {
        _state = state;
        _logger = factory.CreateLogger<UserServiceGrain>();
    }
    
    public async Task AddOrUpdateUserAsync(string userId, UserRecord user)
    {
        var userGrain = GrainFactory.GetGrain<IUserGrain>(userId);
        if (!await userGrain.ExistsAsync(userId))
        {
            throw new Exception("User does not exist");
        }

        _state.State.Users[user.Id] = user;
        await _state.WriteStateAsync();
    }

    public Task<IList<UserRecord>> GetUsersAsync()
    {
        return Task.FromResult<IList<UserRecord>>(_state.State.Users.Values.ToList());
    }

    public Task<UserRecord?> GetUserAsync(string userId)
    {
        var userGrain = GrainFactory.GetGrain<IUserGrain>(userId);
        return userGrain.GetDetailsAsync();
    }
}
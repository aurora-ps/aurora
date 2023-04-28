using Aurora.Grains.Services;
using Aurora.Interfaces;
using Microsoft.Extensions.Logging;
using Orleans.Concurrency;

namespace Aurora.Grains;

public class UserGrain : Grain, IUserGrain
{
    private readonly ILogger<UserGrain> _logger;
    private readonly IUserDataService _userDataDataService;
    private UserRecord _state = new();

    public UserGrain(
        IUserDataService userDataDataService,
        ILoggerFactory factory)
    {
        _userDataDataService = userDataDataService;
        _logger = factory.CreateLogger<UserGrain>();
    }

    [ReadOnly]
    public Task<bool> IsInitialized()
    {
        return Task.FromResult(string.IsNullOrEmpty(_state.Id));
    }

    [ReadOnly]
    public Task<UserRecord?> GetDetailsAsync()
    {
        return Task.FromResult(string.IsNullOrEmpty(_state.Id) ? null : _state);
    }

    public async Task<UserRecord?> AddAsync(string name, string email)
    {
        _state = new UserRecord
        {
            Id = this.GetPrimaryKeyString(),
            Name = name,
            Email = email
        };

        await _userDataDataService.AddAsync(_state);

        return await GetDetailsAsync();
    }

    public async Task<bool> ExistsAsync(string userId)
    {
        return await GetDetailsAsync() != null;
    }

    public override async Task OnActivateAsync(CancellationToken cancellationToken)
    {
        // Check to see if there's a Data record for this.
        if (!string.IsNullOrEmpty(this.GetPrimaryKeyString()))
        {
            var record = await _userDataDataService.GetAsync(this.GetPrimaryKeyString());
            if (record != null) _state = record;
        }

        await base.OnActivateAsync(cancellationToken);
    }
}
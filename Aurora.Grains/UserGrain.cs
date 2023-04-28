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
        return Task.FromResult(!string.IsNullOrEmpty(_state.Id));
    }

    public async Task<UserRecord?> GetDetailsAsync()
    {
        // If not initialized, try to load from the data store.
        if(!await IsInitialized())
            await UpdateFromDataStore();

        return string.IsNullOrEmpty(_state.Id) ? null : _state;
    }

    public async Task<bool> DeleteAsync()
    {
        if (await IsInitialized())
        {
            await _userDataDataService.DeleteAsync(this.GetPrimaryKeyString());
            _state = new UserRecord();
            return true;
        }
        return false;
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

    public override async Task OnActivateAsync(CancellationToken cancellationToken)
    {
        // Check to see if there's a Data record for this.
        if (!string.IsNullOrEmpty(this.GetPrimaryKeyString()))
        {
            await UpdateFromDataStore();
        }

        await base.OnActivateAsync(cancellationToken);
    }

    private async Task UpdateFromDataStore()
    {
        var record = await _userDataDataService.GetAsync(this.GetPrimaryKeyString());
        if (record != null) _state = record;
    }
}
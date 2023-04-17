using Aurora.Data.Interfaces;
using Aurora.Interfaces;
using Microsoft.Extensions.Logging;
using Orleans.Concurrency;

namespace Aurora.Grains;

public class UserGrain : Grain, IUserGrain
{
    private readonly ILogger<UserGrain> _logger;
    private readonly IDataService<UserRecord, string> _userDataService;
    private UserRecord _state = new();

    public UserGrain(
        IDataService<UserRecord, string> userDataService,
        ILoggerFactory factory)
    {
        _userDataService = userDataService;
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

        await _userDataService.AddOrUpdateAsync(_state.Id, _state);

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
            var record = await _userDataService.GetAsync(this.GetPrimaryKeyString());
            if (record != null) _state = record;
        }

        await base.OnActivateAsync(cancellationToken);
    }
}
using Aurora.Data.Interfaces;
using Aurora.Interfaces;

namespace Aurora.Grains.Services;

/// <summary>
///     Simple in-memory data service for testing.
/// </summary>
public sealed class UserDataService : IDataService<UserRecord, string>
{
    private static readonly Dictionary<string, UserRecord> _users = new();

    public Task AddOrUpdateAsync(string id, UserRecord data)
    {
        return Task.Run(() => _users[id] = data);
    }

    public Task<List<UserRecord>> GetAllAsync()
    {
        return Task.FromResult(_users.Values.ToList());
    }

    public Task<UserRecord?> GetAsync(string TKey)
    {
        _users.TryGetValue(TKey, out var user);
        return Task.FromResult(user);
    }
}
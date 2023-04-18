using Aurora.Data.Interfaces;
using Aurora.Interfaces;

namespace Aurora.Grains.Services;

/// <summary>
///     Simple in-memory data service for testing.
/// </summary>
public sealed class UserDataService : IDataService<UserRecord, string>
{
    private static readonly Dictionary<string, UserRecord> Users = new();

    public Task<UserRecord> AddAsync(UserRecord data)
    {
        return Task.Run(() =>
        {
            Users[data.Id] = data;
            return data;
        });
    }

    public Task<IList<UserRecord>> GetAllAsync()
    {
        return Task.FromResult((IList<UserRecord>)Users.Values.ToList());
    }

    public Task<UserRecord?> GetAsync(string key)
    {
        Users.TryGetValue(key, out var user);
        return Task.FromResult(user);
    }
}
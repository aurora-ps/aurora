using System.Collections.Concurrent;
using Aurora.Interfaces;

namespace Aurora.Grains.Services;

public class OrganizationDataService : IOrganizationDataService
{
    private static readonly ConcurrentDictionary<string, OrganizationRecord> Organizations = new();

    public async Task<OrganizationRecord> AddAsync(OrganizationRecord data)
    {
        return await Task.Run(() =>
        {
            Organizations.TryAdd(data.Id, data);
            return data;
        });
    }

    public Task<IList<OrganizationRecord>> GetAllAsync()
    {
        return Task.FromResult((IList<OrganizationRecord>)Organizations.Values.ToList());
    }

    public async Task<OrganizationRecord?> GetAsync(string key)
    {
        return await Task.Run(() =>
        {
            Organizations.TryGetValue(key, out var organization);
            return organization;
        });
    }
}
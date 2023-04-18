using System.Collections.Concurrent;
using System.Transactions;
using Aurora.Interfaces;

namespace Aurora.Grains;

public interface IOrganizationDataService
{
    Task<OrganizationRecord?> GetOrganizationAsync(string id);
    bool AddOrganization(OrganizationRecord organization);
    Task<IList<OrganizationRecord>> GetOrganizationsAsync();
}

public class OrganizationDataService : IOrganizationDataService
{
    private readonly IGrainFactory _grainFactory;
    private static readonly ConcurrentDictionary<string, OrganizationRecord> Organizations = new();

    public OrganizationDataService(IGrainFactory grainFactory)
    {
        _grainFactory = grainFactory;
    }

    async Task<OrganizationRecord?> IOrganizationDataService.GetOrganizationAsync(string id)
    {
        var grain = _grainFactory.GetGrain<IOrganizationGrain>(id);
        if (grain is not null)
        {
            // Try and get the organization from the grain first
            var grainDetails = await grain.GetDetailsAsync();
            if (grainDetails is not null)
            {
                return grainDetails;
            }

            // If the grain doesn't have the organization, try and get it from the data store
            var organization = GetOrganizationFromDataStore(id);
            if (organization is not null)
            {
                // If the organization was found in the data store, add it to the grain
                await grain.SetDetailsAsync(organization);
                return organization;
            }
        }

        return null;
    }

    private static OrganizationRecord? GetOrganizationFromDataStore(string id)
    {
        Organizations.TryGetValue(id, out var organization);
        return organization;
    }

    bool IOrganizationDataService.AddOrganization(OrganizationRecord organization)
    {
        return Organizations.TryAdd(organization.Id, organization);
    }

    Task<IList<OrganizationRecord>> IOrganizationDataService.GetOrganizationsAsync()
    {
        return Task.FromResult((IList<OrganizationRecord>)Organizations.Values.ToList());
    }
}
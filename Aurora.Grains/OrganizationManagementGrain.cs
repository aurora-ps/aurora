using Aurora.Grains.Services;
using Aurora.Interfaces;
using Microsoft.Extensions.Logging;
using Orleans.Concurrency;

namespace Aurora.Grains;

[Reentrant]
public class OrganizationManagementGrain : Grain, IOrganizationManagementGrain
{
    private readonly ILogger<OrganizationManagementGrain> _logger;

    private readonly IOrganizationDataService _organizationDataService;

    public OrganizationManagementGrain(IOrganizationDataService organizationDataService, ILoggerFactory factory)
    {
        _organizationDataService = organizationDataService;
        _logger = factory.CreateLogger<OrganizationManagementGrain>();
    }

    async Task<OrganizationRecord?> IOrganizationManagementGrain.AddAsync(string name)
    {
        _logger.LogTrace("Creating organization {OrganizationName}", name);
        var organizationGrain = GrainFactory.GetGrain<IOrganizationGrain>(Guid.NewGuid().ToString());
        var organization = await organizationGrain.CreateAsync(name);
        return organization;
    }

    [ReadOnly]
    async Task<OrganizationRecord?> IOrganizationManagementGrain.GetOrganizationAsync(string id)
    {
        var grain = GrainFactory.GetGrain<IOrganizationGrain>(id);

        // Try and get the organization from the grain first
        var grainDetails = await grain.GetDetailsAsync();
        if (grainDetails is null)
        {
            var organization = await _organizationDataService.GetAsync(id);
            if (organization is null) return organization;

            // If the organization was found in the data store, add it to the grain
            await grain.SetDetailsAsync(organization);

            return organization;
        }

        // If the grain doesn't have the organization, try and get it from the data store
        return grainDetails;
    }

    [ReadOnly]
    Task<IList<OrganizationRecord>> IOrganizationManagementGrain.GetOrganizationsAsync()
    {
        return _organizationDataService.GetAllAsync();
    }
}
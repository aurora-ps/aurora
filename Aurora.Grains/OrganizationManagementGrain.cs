using Aurora.Interfaces;
using Microsoft.Extensions.Logging;
using Orleans.Concurrency;

namespace Aurora.Grains;

[Reentrant]
public class OrganizationManagementGrain : Grain, IOrganizationManagementGrain
{
    private readonly ILogger<OrganizationManagementGrain> _logger;
    private readonly IOrganizationDataService _organizationDataService;
    
    public OrganizationManagementGrain(
        IOrganizationDataService organizationDataService,
        ILoggerFactory factory)
    {
        _organizationDataService = organizationDataService;
        _logger = factory.CreateLogger<OrganizationManagementGrain>();
    }

    [ReadOnly]
    Task<IList<OrganizationRecord>> IOrganizationManagementGrain.GetOrganizationsAsync()
    {
        return _organizationDataService.GetOrganizationsAsync();
    }

    [ReadOnly]
    Task<OrganizationRecord?> IOrganizationManagementGrain.GetOrganizationAsync(string id)
    {
        return _organizationDataService.GetOrganizationAsync(id);
    }

    async Task<OrganizationRecord?> IOrganizationManagementGrain.AddAsync(string name)
    {
        var organizationGrain = GrainFactory.GetGrain<IOrganizationGrain>(Guid.NewGuid().ToString());
        var organization = await organizationGrain.CreateAsync(name);
        if (organization is not null) _organizationDataService.AddOrganization(organization);
        return organization;
    }

    Task<bool> IOrganizationManagementGrain.AddToCollectionAsync(OrganizationRecord record)
    {
        try
        {
            if (_organizationDataService.GetOrganizationAsync(record.Id) is null)
            {
                _organizationDataService.AddOrganization(record);
            }
            return Task.FromResult(true);

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Task.FromResult(false);
        }
    }
}
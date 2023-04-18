using Aurora.Interfaces;
using Microsoft.Extensions.Logging;
using Orleans.Concurrency;

namespace Aurora.Grains;

[Reentrant]
public class OrganizationGrain : Grain, IOrganizationGrain
{
    private readonly IOrganizationDataService _organizationDataService;
    private readonly ILogger<OrganizationGrain> _logger;
    private OrganizationRecord? _organization = null;

    public OrganizationGrain(
        IOrganizationDataService organizationDataService,
        ILoggerFactory factory)
    {
        _organizationDataService = organizationDataService;
        _logger = factory.CreateLogger<OrganizationGrain>();
    }

    [ReadOnly]
    Task<OrganizationRecord?> IOrganizationGrain.GetDetailsAsync()
    {
        return Task.FromResult(_organization);
    }

    async Task<OrganizationRecord?> IOrganizationGrain.CreateAsync(string name)
    {

        var _organization = new OrganizationRecord
        {
            Id = this.GetPrimaryKeyString(),
            Name = name
        };

        if (_organizationDataService.AddOrganization(_organization))
        {
            return _organization;
        }

        return null;
    }

    public Task SetDetailsAsync(OrganizationRecord organization)
    {
        return Task.Run(() => _organization = organization);
    }
}
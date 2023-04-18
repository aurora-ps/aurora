using Aurora.Interfaces;
using Microsoft.Extensions.Logging;
using Orleans.Concurrency;

namespace Aurora.Grains;

[Reentrant]
public class OrganizationGrain : Grain, IOrganizationGrain
{
    private readonly ILogger<OrganizationGrain> _logger;
    private readonly IOrganizationDataService _organizationDataService;
    private OrganizationRecord? _organization;

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
        this._logger.LogTrace("Getting organization details for {OrganizationId}", this.GetPrimaryKeyString());
        return Task.FromResult(_organization);
    }

    async Task<OrganizationRecord?> IOrganizationGrain.CreateAsync(string name)
    {
        this._logger.LogTrace("Creating organization {OrganizationName}", name);
        var organization = new OrganizationRecord
        {
            Id = this.GetPrimaryKeyString(),
            Name = name
        };

        var addedOrg = await _organizationDataService.AddAsync(organization);
        return _organization = addedOrg;
    }

    public Task SetDetailsAsync(OrganizationRecord organization)
    {
        this._logger.LogTrace("Setting organization details for {OrganizationId}", organization.Id);
        return Task.Run(() => _organization = organization);
    }
}
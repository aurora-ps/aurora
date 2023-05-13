using Aurora.Infrastructure.Data;
using Aurora.Interfaces;
using Aurora.Interfaces.Models.Reporting;
using Microsoft.EntityFrameworkCore;

namespace Aurora.Grains;

public class AgencyManagementGrain : Grain, IAgencyManagementGrain
{
    private readonly ReportDbContext _reportContext;
    private readonly IClusterClient _clusterClient;

    public AgencyManagementGrain(ReportDbContext reportContext, IClusterClient clusterClient)
    {
        _reportContext = reportContext;
        _clusterClient = clusterClient;
    }

    public async Task<IList<AgencyRecord>?> GetAgenciesAsync(string? requestSearch, bool? includeDeleted)
    {
        // get a list of agency keys from the grain
        var agencyKeys = await _reportContext.Agencies
            .Where(a => a.Name.Contains(requestSearch ?? string.Empty))
            .Where(a => includeDeleted == true || a.DeletedOnUtc == null)
            .Select(a => a.Id).ToListAsync();

        // get a list of agency grains from the cluster
        var agencyGrains = agencyKeys.Select(k => _clusterClient.GetGrain<IAgencyGrain>(k)).ToList();

        // get the details for each agency grain
        var agencyDetails = await Task.WhenAll(agencyGrains.Select(g => g.GetDetailsAsync()));

        // return the list of agency details
        return agencyDetails?.OrderBy(_ => _.Name).ToList();
    }
}
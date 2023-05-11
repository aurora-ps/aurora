using Aurora.Interfaces.Models.Reporting;

namespace Aurora.Features.Agency.GetAgencies;

public class GetAgenciesResponse
{
    public IList<AgencyRecord> Agencies { get; set; } = new List<AgencyRecord>();
}
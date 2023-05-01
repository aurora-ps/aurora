using Aurora.Interfaces.Models.Reporting;

namespace Aurora.Features.Agency.GetAgencies;

public class GetAgenciesResponse
{
    public List<AgencyRecord> Agencies { get; set; } = new ();
}
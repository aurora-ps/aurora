namespace Aurora.Features.Agency.GetAgencies;

public class GetAgenciesResponse
{
    public IList<Interfaces.Models.Reporting.Agency> Agencies { get; set; } = new List<Interfaces.Models.Reporting.Agency>();
}
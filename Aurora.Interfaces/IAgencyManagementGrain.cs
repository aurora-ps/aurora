using Aurora.Interfaces.Models.Reporting;

namespace Aurora.Interfaces;

public interface IAgencyManagementGrain : IGrainWithStringKey
{
    Task<IList<AgencyRecord>?> GetAgenciesAsync(string? requestSearch);
}
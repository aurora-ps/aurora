using Aurora.Interfaces.Models.Reporting;

namespace Aurora.Interfaces;

public interface IAgencyGrain : IGrainWithStringKey
{
    Task<AgencyRecord?> GetDetailsAsync();

    Task<IList<IncidentTypeRecord>?> GetIncidentTypesAsync();

    Task<bool> RemoveIncidentTypeAsync(IncidentTypeRecord? incidentType);

    Task<bool> AddIncidentTypeAsync(IncidentTypeRecord? incidentType);

    Task<string> SetAgencyName(string name);

    Task<bool> SaveChangesAsync();
    Task UpdateIncidentTypeAsync(IncidentTypeRecord requestIncidentType);
    Task DeleteAsync();
    Task UnDeleteAsync();
}
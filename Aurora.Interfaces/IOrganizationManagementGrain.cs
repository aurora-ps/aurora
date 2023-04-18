namespace Aurora.Interfaces;

public interface IOrganizationManagementGrain : IGrainWithStringKey
{
    Task<IList<OrganizationRecord>> GetOrganizationsAsync();

    Task<OrganizationRecord?> GetOrganizationAsync(string id);

    Task<OrganizationRecord?> AddAsync(string name);
}
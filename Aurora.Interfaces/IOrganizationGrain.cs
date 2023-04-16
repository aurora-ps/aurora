namespace Aurora.Interfaces;

public interface IOrganizationGrain : IGrainWithStringKey
{
    Task<bool> IsInitialized();

    Task<OrganizationRecord?> GetDetailsAsync();

    Task<OrganizationRecord?> AddAsync(string name);
}
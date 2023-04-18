namespace Aurora.Interfaces;

public interface IOrganizationGrain : IGrainWithStringKey
{
    Task<OrganizationRecord?> GetDetailsAsync();

    Task<OrganizationRecord?> CreateAsync(string name);

    Task SetDetailsAsync(OrganizationRecord organization);
}
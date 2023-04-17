namespace Aurora.Interfaces;

public interface IServerGrain : IGrainWithStringKey
{
    Task AddOrganizationToServer(OrganizationRecord organization);

    Task<IList<OrganizationRecord>> GetOrganizations();
}
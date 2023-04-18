using Aurora.Data.Interfaces;
using Aurora.Interfaces;

namespace Aurora.Grains.Services;

public interface IOrganizationDataService : IDataService<OrganizationRecord, string>
{
}
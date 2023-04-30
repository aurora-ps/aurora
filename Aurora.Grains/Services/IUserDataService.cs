using Aurora.Data.Interfaces;
using Aurora.Interfaces;

namespace Aurora.Grains.Services;

public interface IUserDataService : IDataService<UserRecord, string>
{
    Task<UserRecord?> GetByUserNameAsync(string userName);
    Task DeleteAsync(string getPrimaryKeyString);
}
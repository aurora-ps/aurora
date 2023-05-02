using Aurora.Interfaces.Models;

namespace Aurora.Interfaces;

public interface IUserGrain : IGrainWithStringKey
{
    Task<bool> IsInitialized();
    Task<UserRecord?> GetDetailsAsync();
    Task<UserRecord?> AddAsync(string name, string email);
    Task<bool> DeleteAsync();
}
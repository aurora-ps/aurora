namespace Aurora.Interfaces;

public interface IUserGrain : IGrainWithStringKey
{
    Task<bool> IsInitialized();
    Task<UserRecord?> GetDetailsAsync();
    Task<UserRecord?> AddAsync(string name, string email);
    Task<bool> ExistsAsync(string userId);
}

public interface IUserServiceGrain : IGrainWithStringKey
{
    Task<UserRecord?> FindByIdAsync(string userId);

    Task<UserRecord?> FindByNameAsync(string userName);

    Task<IList<UserRecord>> GetAllAsync();
}
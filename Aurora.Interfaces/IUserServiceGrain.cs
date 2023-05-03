namespace Aurora.Interfaces;

public interface IUserServiceGrain : IGrainWithStringKey
{
    Task<UserRecord?> FindByIdAsync(string userId);

    Task<UserRecord?> FindByNameAsync(string userName);

    Task<IList<UserRecord>> GetAllAsync();

    Task<UserRecord?> AddAsync(UserRecord userRecord);
}
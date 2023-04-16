namespace Aurora.Interfaces;

public interface IUserServiceGrain : IGrainWithStringKey
{
    Task AddOrUpdateUserAsync(string userId, UserRecord user);

    Task<IList<UserRecord>> GetUsersAsync();

    Task<UserRecord?> GetUserAsync(string userId);
}
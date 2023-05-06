namespace Aurora.Interfaces;

public interface IUserGrain : IGrainWithStringKey
{
    Task<bool> IsInitialized();
    
    Task<UserRecord?> GetDetailsAsync();

    Task<bool> DeleteAsync();
    Task SetLastLoginAsync(DateTime utcNow);
}
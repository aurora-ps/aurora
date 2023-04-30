namespace Aurora.Interfaces;

public interface IUserServiceGrain : IGrainWithStringKey
{
    Task<UserRecord?> FindByIdAsync(string userId);

    Task<UserRecord?> FindByNameAsync(string userName);

    Task<IList<UserRecord>> GetAllAsync();
}

public interface IReportServiceGrain : IGrainWithStringKey
{
    Task<bool> ReportExistsAsync(string reportId);
}
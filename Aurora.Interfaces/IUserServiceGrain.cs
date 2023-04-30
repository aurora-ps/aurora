using Aurora.Interfaces.Models.Reporting;

namespace Aurora.Interfaces;

public interface IUserServiceGrain : IGrainWithStringKey
{
    Task<UserRecord?> FindByIdAsync(string userId);

    Task<UserRecord?> FindByNameAsync(string userName);

    Task<IList<UserRecord>> GetAllAsync();
}

public interface IReportServiceGrain : IGrainWithStringKey
{
    Task<IList<ReportRecord>> GetAllAsync();
    Task<bool> ReportExistsAsync(string reportId);
}

public interface IReportGrain : IGrainWithStringKey
{
    Task<ReportRecord?> GetAsync(string reportId);
    Task<ReportRecord?> AddOrUpdateAsync(ReportRecord data);
}
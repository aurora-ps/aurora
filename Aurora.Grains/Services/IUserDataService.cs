using Aurora.Data.Interfaces;
using Aurora.Interfaces;
using Aurora.Interfaces.Models.Reporting;

namespace Aurora.Grains.Services;

public interface IUserDataService : IDataService<UserRecord, string>
{
    Task<UserRecord?> GetByUserNameAsync(string userName);
    Task DeleteAsync(string getPrimaryKeyString);
}

public interface IReportDataService : IDataService<Report, string>
{
    Task<Report> AddOrUpdateAsync(Report record);
}
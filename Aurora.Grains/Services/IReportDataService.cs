using Aurora.Data.Interfaces;
using Aurora.Interfaces.Models.Reporting;

namespace Aurora.Grains.Services;

public interface IReportDataService : IDataService<Report, string>
{
    Task<Report> AddOrUpdateAsync(Report record);
    Task<bool> UnDeleteAsync(string reportId);
    Task<DateTime?> DeleteAsync(string getPrimaryKeyString);

    Task<IList<Report>> GetAllAsync(bool includeDeleted = false);
    Task<IList<Report>> GetForUserAsync(string? requestUserId, bool requestShowHidden);
}
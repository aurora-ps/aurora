using Aurora.Interfaces.Models.Reporting;

namespace Aurora.Interfaces;

public interface IReportGrain : IGrainWithStringKey
{
    Task<ReportRecord?> GetAsync();
    Task<ReportRecord?> AddOrUpdateAsync(ReportRecord data);
    Task<bool> IsPersistedAsync();
    Task<bool> UnDeleteAsync();
    Task<DateTime?> DeleteAsync();
}
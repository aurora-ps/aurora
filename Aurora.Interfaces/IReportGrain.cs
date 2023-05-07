using Aurora.Interfaces.Models.Reporting;

namespace Aurora.Interfaces;

public interface IReportGrain : IGrainWithStringKey
{
    Task<ReportRecord?> GetAsync();
    Task<bool> AddOrUpdateAsync(ReportRecord record);
    Task<bool> UnDeleteAsync();
    Task<DateTime?> DeleteAsync();
}
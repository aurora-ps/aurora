using Aurora.Interfaces.Models.Reporting;

namespace Aurora.Interfaces;

public interface IReportGrain : IGrainWithStringKey
{
    Task<ReportRecord?> GetAsync(string reportId);
    Task<ReportRecord?> AddOrUpdateAsync(ReportRecord data);
    Task<bool> IsPersistedAsync();
}
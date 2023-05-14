using Aurora.Interfaces.Models.Reporting;

namespace Aurora.Interfaces;

public interface IReportServiceGrain : IGrainWithStringKey
{
    Task<IList<ReportSummaryRecord>> GetAllAsync(bool includeDeleted = false);
    Task<bool> ReportExistsAsync(string reportId);
    Task<IList<ReportSummaryRecord>> GetUserReportsAsync(string? requestUserId, bool requestShowHidden);
}
using Aurora.Interfaces.Models.Reporting;

namespace Aurora.Interfaces;

public interface IReportServiceGrain : IGrainWithStringKey
{
    Task<IList<ReportRecord>> GetAllAsync(bool includeDeleted = false);
    Task<bool> ReportExistsAsync(string reportId);
}
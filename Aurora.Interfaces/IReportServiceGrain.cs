using Aurora.Interfaces.Models.Reporting;

namespace Aurora.Interfaces;

public interface IReportServiceGrain : IGrainWithStringKey
{
    Task<IList<ReportRecord>> GetAllAsync();
    Task<bool> ReportExistsAsync(string reportId);
}
using Aurora.Data.Interfaces;
using Aurora.Interfaces.Models.Reporting;

namespace Aurora.Grains.Services;

public interface IReportDataService : IDataService<Report, string>
{
    Task<Report> AddOrUpdateAsync(Report record);
}
using Aurora.Grains.Services;
using Aurora.Interfaces;
using Aurora.Interfaces.Models.Reporting;
using Microsoft.Extensions.Logging;

namespace Aurora.Grains;

public class ReportServiceGrain : Grain, IReportServiceGrain
{
    private readonly ILogger<ReportServiceGrain> _logger;
    private readonly IReportDataService _reportDataService;

    public ReportServiceGrain(IReportDataService reportDataService, ILogger<ReportServiceGrain> logger)
    {
        _reportDataService = reportDataService;
        _logger = logger;
    }

    public async Task<IList<ReportRecord>> GetAllAsync()
    {
        var reports = await _reportDataService.GetAllAsync();

        return reports.Select(r => r.ToReportRecord()).ToList();
    }

    public async Task<bool> ReportExistsAsync(string reportId)
    {
        return await _reportDataService.ExistsAsync(reportId);
    }
}
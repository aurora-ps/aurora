using Aurora.Grains.Services;
using Aurora.Interfaces;
using Aurora.Interfaces.Models.Reporting;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace Aurora.Grains;

public class ReportServiceGrain : Grain, IReportServiceGrain
{
    private readonly ILogger<ReportServiceGrain> _logger;
    private readonly IReportDataService _reportDataService;
    private readonly IMapper _mapper;

    public ReportServiceGrain(IReportDataService reportDataService, IMapper mapper, ILogger<ReportServiceGrain> logger)
    {
        _reportDataService = reportDataService;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IList<ReportRecord>> GetAllAsync(bool includeDeleted = false)
    {
        try
        {
            var reports = await _reportDataService.GetAllAsync(includeDeleted);
            List<ReportRecord> records = new List<ReportRecord>();
            foreach (var report in reports)
            {
                records.Add(_mapper.Map<ReportRecord>(report));
            }

            return records;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }

    public async Task<bool> ReportExistsAsync(string reportId)
    {
        return await _reportDataService.ExistsAsync(reportId);
    }

    public async Task<IList<ReportRecord>> GetUserReportsAsync(string? requestUserId, bool requestShowHidden)
    {
        try
        {
            var reports = await _reportDataService.GetForUserAsync(requestUserId, requestShowHidden);
            return _mapper.Map<IList<ReportRecord>>(reports);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }
}
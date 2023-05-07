using Aurora.Grains.Services;
using Aurora.Infrastructure.Data;
using Aurora.Interfaces;
using Aurora.Interfaces.Models.Reporting;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Aurora.Grains;

public class ReportServiceGrain : Grain, IReportServiceGrain
{
    private readonly ILogger<ReportServiceGrain> _logger;
    private readonly IMapper _mapper;
    private readonly ReportDbContext _reportContext;

    public ReportServiceGrain(ReportDbContext reportContext, IMapper mapper, ILogger<ReportServiceGrain> logger)
    {
        _reportContext = reportContext;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IList<ReportRecord>> GetAllAsync(bool includeDeleted = false)
    {
        try
        {
            var reports = await GetReportQueryable(includeDeleted)
                .ProjectTo<ReportRecord>(_mapper.ConfigurationProvider)
                .ToListAsync();
           
            return reports;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }

    public async Task<bool> ReportExistsAsync(string reportId)
    {
        return await _reportContext.Reports.AnyAsync(_ => _.Id == reportId);
    }

    public async Task<IList<ReportRecord>> GetUserReportsAsync(string? requestUserId, bool requestShowHidden)
    {
        try
        {
            var reports = await this.GetReportQueryable(requestShowHidden)
                .Where(_ => requestUserId == null || _.CreatedByUserId == requestUserId)
                .ProjectTo<ReportRecord>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return reports;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }

    private IQueryable<Report> GetReportQueryable(bool includeDeleted)
    {
        return _reportContext.Reports
            .Where(_ => includeDeleted || _.DeletedOnUtc == null);
    }
}
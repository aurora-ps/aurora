using Aurora.Infrastructure.Data;
using Aurora.Interfaces;
using Aurora.Interfaces.Models.Reporting;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Aurora.Grains;

public class ReportGrain : Grain, IReportGrain
{
    private readonly ReportDbContext _context;
    private readonly ILogger<ReportGrain> _logger;
    private readonly IMapper _mapper;
    private ReportRecord? _state;

    public ReportGrain(ReportDbContext context, IMapper mapper, ILogger<ReportGrain> logger)
    {
        _context = context;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<ReportRecord?> GetAsync()
    {
        return await Task.FromResult(_state);
    }

    public async Task<bool> AddOrUpdateAsync(ReportRecord record)
    {
        try
        {
            if (record.Id != this.GetPrimaryKeyString())
                throw new InvalidOperationException("Report ID does not match grain ID.");

            var report = await GetRecordForUpdateAsync(record);

            if (report != null)
            {
                _mapper.Map(record, report);
            }
            else
            {
                report = _mapper.Map<Report>(record);
                await _context.AddAsync(report);
            }

            var results = await _context.SaveChangesAsync();
            await RefreshStateAsync();

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }

    public async Task<bool> UnDeleteAsync()
    {
        var report = await _context.Reports.FirstOrDefaultAsync(r => r.Id == this.GetPrimaryKeyString());
        if (report == null) return false;

        report.DeletedOnUtc = null;
        await _context.SaveChangesAsync();

        await RefreshStateAsync();

        return true;
    }

    public async Task<DateTime?> DeleteAsync()
    {
        var report = await _context.Reports.FirstOrDefaultAsync(r => r.Id == this.GetPrimaryKeyString());
        if (report == null)
            return null;

        if (!report.DeletedOnUtc.HasValue)
        {
            report.DeletedOnUtc = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            await RefreshStateAsync();

            return report.DeletedOnUtc;
        }

        return report.DeletedOnUtc;
    }

    public override async Task OnActivateAsync(CancellationToken cancellationToken)
    {
        await RefreshStateAsync();

        _state ??= new ReportRecord();

        await base.OnActivateAsync(cancellationToken);
    }

    private async Task RefreshStateAsync()
    {
        var persistedRecord = await GetAsync(this.GetPrimaryKeyString());

        // Only refresh the state if the persisted record is different than the current state.
        if(persistedRecord != null)
            _state = persistedRecord;

    }

    private async Task<ReportRecord?> GetAsync(string reportId)
    {
        var record = await _context.Reports.ProjectTo<ReportRecord>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(r => r.Id == reportId);
        return record;
    }

    private async Task<Report?> GetRecordForUpdateAsync(ReportRecord record)
    {
        return await _context.Reports
            .Include(r => r.Agency)
            .Include(r => r.IncidentType)
            .Include(r => r.Location)
            .Include(r => r.MinistryOpportunity)
            .Include(r => r.People).ThenInclude(p => p.Location)
            .Include(r => r.People).ThenInclude(p => p.PhoneNumber)
            .FirstOrDefaultAsync(_ => _.Id == record.Id);
    }
}
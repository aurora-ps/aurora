using Aurora.Grains.Services;
using Aurora.Interfaces;
using Aurora.Interfaces.Models.Reporting;
using Microsoft.Extensions.Logging;

namespace Aurora.Grains;

public class ReportGrain : Grain, IReportGrain
{
    private readonly ILogger<ReportGrain> _logger;
    private readonly IReportDataService _reportDataService;
    private ReportRecord? _state;

    public ReportGrain(IReportDataService reportDataService, ILogger<ReportGrain> logger)
    {
        _reportDataService = reportDataService;
        _logger = logger;
    }

    public override async Task OnActivateAsync(CancellationToken cancellationToken)
    {
        if (this._state == null)
        {
            var record = await this.GetAsync(this.GetPrimaryKeyString());
            if (record != null)
            {
                this._state = record;
            }
            
        }

        await base.OnActivateAsync(cancellationToken);
    }

    public async Task<ReportRecord?> GetAsync()
    {
        if (!await IsPersistedAsync())
        {
            _state = await this.GetAsync(this.GetPrimaryKeyString());
        }

        return this._state;
    }

    private async Task<ReportRecord?> GetAsync(string reportId)
    {
        var report = await _reportDataService.GetAsync(reportId);

        return _state = report?.ToReportRecord();
    }

    public async Task<ReportRecord?> AddOrUpdateAsync(ReportRecord data)
    {
        if (data.Id != this.GetPrimaryKeyString())
        {
            throw new InvalidOperationException("Report ID does not match grain ID.");
        }

        var record = data.ToReport();
        var results = await _reportDataService.AddOrUpdateAsync(record);

        if (results != null)
        {
            this._state = await GetAsync(this.GetPrimaryKeyString());
            return this._state;
        }

        return null;
    }

    public Task<bool> IsPersistedAsync()
    {
        return Task.FromResult(this._state != null);
    }

    public async Task<bool> UnDeleteAsync()
    {
        if (await _reportDataService.UnDeleteAsync(this.GetPrimaryKeyString()))
        {
            this._state = await GetAsync(this.GetPrimaryKeyString());
            return true;
        }
        return false;
    }

    public async Task<DateTime?> DeleteAsync()
    {
        DateTime? deletedOnUtc = await _reportDataService.DeleteAsync(this.GetPrimaryKeyString());
        if (deletedOnUtc.HasValue)
        {
            this._state = await GetAsync(this.GetPrimaryKeyString());
        }

        return deletedOnUtc;
    }
}
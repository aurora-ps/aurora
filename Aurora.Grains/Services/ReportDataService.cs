using Aurora.Infrastructure.Data;
using Aurora.Interfaces.Models.Reporting;
using Microsoft.EntityFrameworkCore;

namespace Aurora.Grains.Services;

public class ReportDataService : IReportDataService
{
    private readonly ReportDbContext _reportContext;

    public ReportDataService(ReportDbContext reportContext)
    {
        _reportContext = reportContext;
    }
    public Task<Report> AddAsync(Report data)
    {
        throw new NotImplementedException();
    }

    public async Task<IList<Report>> GetAllAsync()
    {
        return await _reportContext.Reports.Include(r => r.Agency)
            .Include(r => r.IncidentType)
            .Include(r => r.Location)
            .Include(r => r.People).ThenInclude(p => p.Location)
            .Include(r => r.People).ThenInclude(p => p.PhoneNumber)
            .ToListAsync();
    }

    public Task<Report?> GetAsync(string key)
    {
        return _reportContext.Reports.AsNoTracking()
            .Include(r => r.Agency)
            .Include(r => r.IncidentType)
            .Include(r => r.Location)
            .Include(r => r.People).ThenInclude(p => p.Location)
            .Include(r => r.People).ThenInclude(p => p.PhoneNumber)
            .SingleOrDefaultAsync(r => r.Id == key);
    }

    public Task<bool> ExistsAsync(string key)
    {
        return _reportContext.Reports.AnyAsync(r => r.Id == key);
    }

    public async Task<Report> AddOrUpdateAsync(Report record)
    {
        var existing = await _reportContext.Reports.SingleOrDefaultAsync(r => r.Id == record.Id);
        if (existing == null)
        {
            _reportContext.Reports.Add(record);
        }
        else
        {
            existing.AgencyId = record.AgencyId;
            existing.Date = record.Date;
            existing.IncidentTypeId = record.IncidentTypeId;
            existing.Location = record.Location;
            existing.Miles = record.Miles;
            existing.People = record.People;
            existing.Narrative = record.Narrative;
            existing.Time = record.Time;
        }

        await _reportContext.SaveChangesAsync();

        // TODO Automapper and map values
        return record;
    }
}
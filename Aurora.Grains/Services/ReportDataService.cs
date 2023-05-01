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

    public async Task<IList<Report>> GetAllAsync(bool includeDeleted = false)
    {
        var listAsync = await _reportContext.Reports
            .Where(_ => (includeDeleted) || (_.DeletedOnUtc == null))
            .AsNoTracking().Include(r => r.Agency)
            .Include(r => r.IncidentType)
            .Include(r => r.Location)
            .Include(r => r.People).ThenInclude(p => p.Location)
            .Include(r => r.People).ThenInclude(p => p.PhoneNumber)
            .ToListAsync();

        return listAsync;
    }

    public async Task<IList<Report>> GetAllAsync() => await this.GetAllAsync(true);

    public Task<Report?> GetAsync(string key)
    {
        return _reportContext.Reports
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
        var existing = await this.GetAsync(record.Id);
        if (existing == null)
        {
            _reportContext.Reports.Add(record);
        }
        else
        {
            //_reportContext.Attach(existing);
            existing.AgencyId = record.AgencyId;
            existing.Date = record.Date;
            existing.IncidentTypeId = record.IncidentTypeId;
            existing.Location = record.Location;
            existing.Miles = record.Miles;
            existing.Narrative = record.Narrative;
            existing.Time = record.Time;

            UpdatePeople(existing, record.People.ToList());

            //_reportContext.Entry(existing).State = EntityState.Modified;
        }

        await _reportContext.SaveChangesAsync();

        // TODO Automapper and map values
        return record;
    }

    public async Task<bool> UnDeleteAsync(string reportId)
    {
        var record = await _reportContext.Reports.SingleOrDefaultAsync(r => r.Id == reportId);
        if (record == null)
            return false;
        
        record.DeletedOnUtc = null;
        await _reportContext.SaveChangesAsync();

        return true;
    }

    public async Task<DateTime?> DeleteAsync(string reportId)
    {
        var utcNow = DateTime.UtcNow;

        var record = await _reportContext.Reports.SingleOrDefaultAsync(r => r.Id == reportId);
        if (record == null)
            return null;
        
        record.DeletedOnUtc = utcNow;
        await _reportContext.SaveChangesAsync();

        return utcNow;
    }

    private void UpdatePeople(Report report, IList<ReportPerson> recordPeople)
    {
        var existingRecords = report.People.ToList();
        foreach (var person in recordPeople)
        {
            // Add if new
            if (existingRecords.All(p => p.Id != person.Id))
            {
                report.People.Add(person);
            }

            // Update if existing
            if (existingRecords.Any(p => p.Id == person.Id))
            {
                var existingRecord = report.People.Single(p => p.Id == person.Id);
                existingRecord.FirstName = person.FirstName;
                existingRecord.LastName = person.LastName;
                existingRecord.Location = person.Location;
                existingRecord.PhoneNumber = person.PhoneNumber;
                existingRecord.Type = person.Type;
                existingRecord.DateOfBirth = person.DateOfBirth;
            }
        }

        var toRemove = existingRecords.Where(p => recordPeople.All(rp => rp.Id != p.Id)).ToList();
        foreach (var person in toRemove)
        {
            report.People.Remove(person);
        }

    }
}
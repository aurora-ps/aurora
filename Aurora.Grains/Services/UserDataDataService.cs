using Aurora.Infrastructure.Data;
using Aurora.Interfaces;
using Aurora.Interfaces.Models;
using Aurora.Interfaces.Models.Reporting;
using Microsoft.AspNetCore.Identity;
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

    public Task<IList<Report>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Report?> GetAsync(string key)
    {
        throw new NotImplementedException();
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
            existing.Agency = record.Agency;
            existing.AgencyId = record.AgencyId;
            existing.Date = record.Date;
            existing.IncidentType = record.IncidentType;
            existing.IncidentTypeId = record.IncidentTypeId;
            existing.Location = record.Location;
            existing.Miles = record.Miles;
            existing.People = record.People;
            existing.Narrative = record.Narrative;
            existing.Time = record.Time;

            _reportContext.Reports.Update(existing);
        }

        await _reportContext.SaveChangesAsync();

        // TODO Automapper and map values
        return record;
    }
}

/// <summary>
///     Simple in-memory data service for testing.
/// </summary>
public sealed class UserDataDataService : IUserDataService
{
    private readonly UserManager<AuroraUser> _userManager;

    public UserDataDataService(UserManager<AuroraUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<UserRecord> AddAsync(UserRecord data)
    {
        if (_userManager.Users.Any(u => u.Id == data.Id)) throw new InvalidOperationException("User already exists.");

        var user = new AuroraUser
        {
            Id = data.Id,
            UserName = data.Name,
            Email = data.Email
        };

        var result = await _userManager.CreateAsync(user);
        if (!result.Succeeded) throw new InvalidOperationException("Failed to create user.");

        return await GetByUserNameAsync(data.Name);
    }

    public async Task<IList<UserRecord>> GetAllAsync()
    {

        try
        {
            var results = await _userManager.Users.Select(u => new UserRecord
            {
                Id = u.Id,
                Name = u.UserName,
                Email = u.Email
            }).ToListAsync();

            return results;
        }
        catch (Exception ex)
        {

            throw;
        }
    }

    public async Task<UserRecord?> GetAsync(string key)
    {
        var user = await _userManager.FindByIdAsync(key);
        if (user == null) return null;
        return new UserRecord
        {
            Id = user.Id,
            Name = user.UserName,
            Email = user.Email
        };
    }

    public async Task<bool> ExistsAsync(string key)
    {
        var user = await _userManager.FindByIdAsync(key);
        return user != null;
    }

    public async Task<UserRecord?> GetByUserNameAsync(string userName)
    {
        var user = await _userManager.FindByNameAsync(userName);
        if (user == null) return null;
        return new UserRecord
        {
            Id = user.Id,
            Name = user.UserName,
            Email = user.Email
        };
    }

    public async Task DeleteAsync(string key)
    {
        var user = await _userManager.FindByIdAsync(key);
        if (user == null) return;

        var result = await _userManager.DeleteAsync(user);
    }
}
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

    public Task<IList<ReportRecord>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<bool> ReportExistsAsync(string reportId)
    {
        return await _reportDataService.ExistsAsync(reportId);
    }
}

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

    public Task<ReportRecord?> GetAsync(string reportId)
    {
        throw new NotImplementedException();
    }

    public async Task<ReportRecord?> AddOrUpdateAsync(ReportRecord data)
    {
        if (data.Id != this.GetPrimaryKeyString())
        {
            throw new InvalidOperationException("Report ID does not match grain ID.");
        }

        var record = GetReportFromReportRecord(data);
        var results = await _reportDataService.AddOrUpdateAsync(record);

        if (results != null)
        {
            this._state = await GetAsync(this.GetPrimaryKeyString());
            return this._state;
        }

        return null;
    }

    private Report GetReportFromReportRecord(ReportRecord reportRecord)
    {
        var report = new Report
        {
            Id = reportRecord.Id,
            AgencyId = reportRecord.Agency.Id,
            Date = reportRecord.Date,
            IncidentTypeId = reportRecord.IncidentType.Id,
            Location = reportRecord.Location != null ? new Location
            {
                Address = reportRecord.Location.Address,
                City = reportRecord.Location.City,
                State = reportRecord.Location.State,
                Zip = reportRecord.Location.Zip
            } : null,
            Miles = reportRecord.Miles,
            Narrative = reportRecord.Narrative,
            People = reportRecord.People.Select(_ => new ReportPerson
            {
                FirstName = _.FirstName,
                LastName = _.LastName,
                DateOfBirth = _.DateOfBirth,
                Location = _.Location != null ?
                    new Location
                    {
                        Address = _.Location.Address,
                        City = _.Location.City,
                        State = _.Location.State,
                        Zip = _.Location.Zip
                    } : null,
                PhoneNumber = _.PhoneNumber != null ?
                                        new PhoneNumber
                                        {
                        Number = _.PhoneNumber.Number,
                        Type = _.PhoneNumber.Type
                    } : null,
                Type = _.Type
            }).ToList(),
            Time = reportRecord.Time
        };
        return report;
    }
}

public class UserServiceGrain : Grain, IUserServiceGrain
{
    private readonly ILogger<UserServiceGrain> _logger;
    private readonly IUserDataService _userDataService;

    public UserServiceGrain(IUserDataService userDataService, ILogger<UserServiceGrain> logger)
    {
        _userDataService = userDataService;
        _logger = logger;
    }

    public Task<UserRecord?> FindByIdAsync(string userId)
    {
        return _userDataService.GetAsync(userId);
    }

    public Task<UserRecord?> FindByNameAsync(string userName)
    {
        return _userDataService.GetByUserNameAsync(userName);
    }

    public Task<IList<UserRecord>> GetAllAsync()
    {
        return _userDataService.GetAllAsync();
    }
}
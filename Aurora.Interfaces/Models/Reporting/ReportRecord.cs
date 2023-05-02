namespace Aurora.Interfaces.Models.Reporting;

[GenerateSerializer]
[Immutable]
public sealed record ReportRecord : IReportRecord
{
    [Id(0)] public string Id { get; set; } = Guid.NewGuid().ToString();
    [Id(1)] public string UserId { get; set; }

    [Id(2)] public DateTime? Date { get; set; }
    [Id(3)] public TimeSpan? Time { get; set; }
    [Id(4)] public AgencyRecord Agency { get; set; } = new ();
    [Id(5)] public IncidentTypeRecord IncidentType { get; set; } = new ();
    [Id(6)] public double? Miles { get; set; }
    [Id(7)] public LocationRecord Location { get; set; } = new ();
    [Id(8)] public string Narrative { get; set; } = string.Empty;
    [Id(9)] public IList<ReportPersonRecord> People { get; set; } = new List<ReportPersonRecord>();

    [Id(10)] public DateTime? DeletedOnUtc { get; set; }
    [Id(11)] public DateTime CreatedOnUtc { get; set; }
    [Id(12)] public DateTime? ClearedDate { get; set; }
    [Id(13)] public TimeSpan? ClearedTime { get; set; }
    
    [Id(14)] public MinistryOpportunityRecord MinistryOpportunity { get; set; } = new ();

    public string TimeDisplay => Time == null ? string.Empty : $"{Time.Value.Hours:00}:{Time.Value.Minutes:00}";
}

public static class ReportExtensions{
    public static ReportRecord ToReportRecord(this IReport report)
    {
        return new ReportRecord
        {
            Agency = report.Agency == null
                ? null
                : new AgencyRecord
                {
                    Id = report.Agency.Id,
                    Name = report.Agency.Name
                },
            Date = report.Date,
            Time = report.Time,
            ClearedDate = report.ClearedDate,
            ClearedTime = report.ClearedTime,
            Id = report.Id,
            CreatedOnUtc = report.CreatedOnUtc,
            DeletedOnUtc = report.DeletedOnUtc,
            IncidentType = report.IncidentType == null
                ? null
                : new IncidentTypeRecord
                {
                    Id = report.IncidentType.Id,
                    Name = report.IncidentType.Name,
                    CollectPerson = report.IncidentType.CollectPerson,
                    CollectLocation = report.IncidentType.CollectLocation,
                    CollectTime = report.IncidentType.CollectTime,
                    RequiresTime = report.IncidentType.RequiresTime,
                },
            Location = report.Location == null
                ? null
                : new LocationRecord
                {
                    Address = report.Location.Address,
                    City = report.Location.City,
                    State = report.Location.State,
                    Zip = report.Location.Zip
                },
            Miles = report.Miles,
            Narrative = report.Narrative,
            People = report.People.Select(_ => new ReportPersonRecord
                {
                    Id = _.Id,
                    FirstName = _.FirstName,
                    LastName = _.LastName,
                    DateOfBirth = _.DateOfBirth,
                    Location = _.Location == null
                        ? null
                        : new LocationRecord
                        {
                            Address = _.Location.Address,
                            City = _.Location.City,
                            State = _.Location.State,
                            Zip = _.Location.Zip
                        },
                    Type = _.Type,
                    PhoneNumber = _.PhoneNumber == null ? null : new PhoneNumberRecord
                    {
                        Number = _.PhoneNumber.Number,
                        Type = _.PhoneNumber.Type
                    }
                }
            ).ToList(),
            MinistryOpportunity = report.MinistryOpportunity

        };
    }

    public static Report ToReport(this IReportRecord reportRecord)
    {
        var report = new Report
        {
            Id = reportRecord.Id,
            AuroraUserId = reportRecord.UserId,
            AgencyId = reportRecord.Agency.Id,
            Date = reportRecord.Date,
            CreatedOnUtc = reportRecord.CreatedOnUtc,
            DeletedOnUtc = reportRecord.DeletedOnUtc,
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
                Id = _.Id,
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
            Time = reportRecord.Time,
            ClearedDate = reportRecord.ClearedDate,
            ClearedTime = reportRecord.ClearedTime,
            MinistryOpportunity = reportRecord.MinistryOpportunity
        };
        return report;
    }
}
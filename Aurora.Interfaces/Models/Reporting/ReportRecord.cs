namespace Aurora.Interfaces.Models.Reporting;

[GenerateSerializer]
[Immutable]
public sealed record ReportRecord : IReportRecord
{
    public UserRecord CreatedBy { get; set; }

    public string UserId { get; set; }

    public UserRecord User { get; set; }

    public string TimeDisplay => Date == null ? string.Empty : $"{Date.Value.Hour:00}:{Date.Value.Minute:00}";
    [Id(0)] public string Id { get; set; } = Guid.NewGuid().ToString();

    public string CreatedByUserId { get; set; }

    public DateTime? Date { get; set; }

    public AgencyRecord Agency { get; set; } = new();

    public IncidentTypeRecord IncidentType { get; set; } = new();

    public double? Miles { get; set; }

    public LocationRecord Location { get; set; } = new();

    public string Narrative { get; set; } = string.Empty;

    public IList<ReportPersonRecord> People { get; set; } = new List<ReportPersonRecord>();

    public DateTime? DeletedOnUtc { get; set; }

    public DateTime CreatedOnUtc { get; set; }

    public DateTime? ClearedDate { get; set; }

    public MinistryOpportunityRecord MinistryOpportunity { get; set; } = new();

    public ReportStateEnum State { get; set; } = ReportStateEnum.Draft;

    public int ReportId { get; set; }

    public TimeSpan? IncidentTime
    {
        get
        {
            if (Date == null) return null;

            var currentDate = Date.Value;
            return new TimeSpan(0, currentDate.Hour, currentDate.Minute, currentDate.Second);
        }
        set
        {
            if (Date == null || value == null) return;

            Date = new DateTime(Date.Value.Year, Date.Value.Month, Date.Value.Day,
                value.Value.Hours, value.Value.Minutes, value.Value.Seconds);
        }
    }

    public TimeSpan? ClearedTime
    {
        get
        {
            if (ClearedDate == null) return null;

            var currentDate = ClearedDate.Value;
            return new TimeSpan(0, currentDate.Hour, currentDate.Minute, currentDate.Second);
        }
        set
        {
            if (ClearedDate == null || value == null) return;

            ClearedDate = new DateTime(ClearedDate.Value.Year, ClearedDate.Value.Month,
                ClearedDate.Value.Day, value.Value.Hours, value.Value.Minutes, value.Value.Seconds);
        }
    }
}
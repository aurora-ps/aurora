namespace Aurora.Interfaces.Models.Reporting;

[GenerateSerializer]
[Immutable]
public sealed record ReportRecord : IReportRecord
{
    [Id(0)] public string Id { get; set; } = Guid.NewGuid().ToString();

    [Id(1)] public string CreatedByUserId { get; set; }

    [Id(2)] public DateTime? Date { get; set; }

    [Id(3)] public AgencyRecord Agency { get; set; } = new();

    [Id(4)] public IncidentTypeRecord IncidentType { get; set; } = new();

    [Id(5)] public double? Miles { get; set; }

    [Id(6)] public LocationRecord Location { get; set; } = new();

    [Id(7)] public string Narrative { get; set; } = string.Empty;

    [Id(8)] public IList<ReportPersonRecord> People { get; set; } = new List<ReportPersonRecord>();

    [Id(9)] public DateTime? DeletedOnUtc { get; set; }

    [Id(10)] public DateTime CreatedOnUtc { get; set; }

    [Id(11)] public DateTime? ClearedDate { get; set; }

    [Id(12)] public MinistryOpportunityRecord MinistryOpportunity { get; set; } = new();

    [Id(13)] public ReportStateEnum State { get; set; } = ReportStateEnum.Draft;

    [Id(14)] public int ReportId { get; set; }

    [Id(15)] public UserRecord CreatedBy { get; set; }

    public string TimeDisplay => Date == null ? string.Empty : $"{Date.Value.Hour:00}:{Date.Value.Minute:00}";

    public TimeSpan? IncidentTime
    {
        get
        {
            if (this.Date == null)
            {
                return null;
            }

            var currentDate = this.Date.Value;
            return new TimeSpan(0, currentDate.Hour, currentDate.Minute, currentDate.Second);
        }
        set
        {
            if (this.Date == null || value == null)
            {
                return;
            }

            this.Date = new DateTime(this.Date.Value.Year, this.Date.Value.Month, this.Date.Value.Day,
                value.Value.Hours, value.Value.Minutes, value.Value.Seconds);
        }
    }

    public TimeSpan? ClearedTime
    {
        get
        {
            if (this.ClearedDate == null)
            {
                return null;
            }

            var currentDate = this.ClearedDate.Value;
            return new TimeSpan(0, currentDate.Hour, currentDate.Minute, currentDate.Second);
        }
        set
        {
            if (this.ClearedDate == null || value == null)
            {
                return;
            }

            this.ClearedDate = new DateTime(this.ClearedDate.Value.Year, this.ClearedDate.Value.Month,
                this.ClearedDate.Value.Day, value.Value.Hours, value.Value.Minutes, value.Value.Seconds);
        }
    }
}
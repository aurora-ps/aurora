namespace Aurora.Interfaces.Models.Reporting;

[GenerateSerializer]
[Immutable]
public sealed record ReportSummaryRecord
{
    [Id(0)] public string Id { get; set; }
    public string CreatedByUserId { get; set; }
    public string CreatedByUserName { get; set; }
    public DateTime? Date { get; set; }
    public string AgencyId { get; set; }
    public string AgencyName { get; set; }
    public string IncidentTypeId { get; set; }
    public string IncidentTypeName { get; set; }
    public string LocationAddress { get; set; }
    public string LocationCity { get; set; }
    public string LocationState { get; set; }
    public string LocationZip { get; set; }
    public DateTime? DeletedOnUtc { get; set; }
    public DateTime CreatedOnUtc { get; set; }
    public DateTime? ClearedDate { get; set; }
    public ReportStateEnum State { get; set; }
    public string UserId { get; set; }
    public string UserName { get; set; }
    public string CreatedByDisplay { get; set; }
    public string ReportUserDisplay { get; set; }

    public string AddressDisplay => $"{LocationAddress}, {LocationCity}, {LocationState} {LocationZip}";

    public string TimeDisplay => Date == null ? string.Empty : $"{Date.Value.Hour:00}:{Date.Value.Minute:00}";

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
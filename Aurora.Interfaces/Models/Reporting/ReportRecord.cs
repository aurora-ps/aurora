namespace Aurora.Interfaces.Models.Reporting;

[GenerateSerializer]
[Immutable]
public sealed record ReportRecord
{
    [Id(0)] public DateTime? Date { get; init; }
    [Id(1)] public TimeSpan? Time { get; init; }
    [Id(2)] public AgencyRecord Agency { get; init; }
    [Id(3)] public IncidentTypeRecord IncidentType { get; init; }
    [Id(4)] public double? Miles { get; init; }
    [Id(5)] public LocationRecord Location { get; init; }
    [Id(6)] public string Narrative { get; init; }
    [Id(7)] public IList<ReportPersonRecord> People { get; init; } = new List<ReportPersonRecord>();
}
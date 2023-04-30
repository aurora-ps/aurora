namespace Aurora.Interfaces.Models.Reporting;

public interface IReportRecord
{
    string Id { get; init; }
    DateTime? Date { get; init; }
    TimeSpan? Time { get; init; }
    AgencyRecord Agency { get; init; }
    IncidentTypeRecord IncidentType { get; init; }
    double? Miles { get; init; }
    LocationRecord Location { get; init; }
    string Narrative { get; init; }
    IList<ReportPersonRecord> People { get; init; }
}

[GenerateSerializer]
[Immutable]
public sealed record ReportRecord : IReportRecord
{
    [Id(0)] public string Id { get; init; } = Guid.NewGuid().ToString();
    [Id(1)] public DateTime? Date { get; init; }
    [Id(2)] public TimeSpan? Time { get; init; }
    [Id(3)] public AgencyRecord Agency { get; init; }
    [Id(4)] public IncidentTypeRecord IncidentType { get; init; }
    [Id(5)] public double? Miles { get; init; }
    [Id(6)] public LocationRecord Location { get; init; }
    [Id(7)] public string Narrative { get; init; }
    [Id(8)] public IList<ReportPersonRecord> People { get; init; } = new List<ReportPersonRecord>();
}
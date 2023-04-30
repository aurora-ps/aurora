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
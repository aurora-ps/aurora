namespace Aurora.Interfaces.Models.Reporting;

public interface IReportRecord
{
    string Id { get; set; }

    string UserId { get; set; }

    DateTime? Date { get; set; }

    TimeSpan? Time { get; set; }

    AgencyRecord Agency { get; set; }

    IncidentTypeRecord IncidentType { get; set; }

    double? Miles { get; set; }

    LocationRecord Location { get; set; }

    string Narrative { get; set; }

    IList<ReportPersonRecord> People { get; set; }
}
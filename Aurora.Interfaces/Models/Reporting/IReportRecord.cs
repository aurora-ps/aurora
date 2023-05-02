namespace Aurora.Interfaces.Models.Reporting;

public interface IReportRecord
{
    string Id { get; set; }

    int ReportId { get; set; }

    ReportStateEnum State { get; set; }

    string UserId { get; set; }

    DateTime? Date { get; set; }

    DateTime? ClearedDate { get; set; }

    TimeSpan? ClearedTime { get; set; }

    AgencyRecord Agency { get; set; }

    IncidentTypeRecord IncidentType { get; set; }

    double? Miles { get; set; }

    LocationRecord? Location { get; set; }

    string Narrative { get; set; }

    IList<ReportPersonRecord> People { get; set; } 

    DateTime? DeletedOnUtc { get; set; }

    DateTime CreatedOnUtc { get; set; }

    MinistryOpportunityRecord MinistryOpportunity { get; set; }
    TimeSpan? IncidentTime { get; set; }
}
namespace Aurora.Interfaces.Models.Reporting;

public interface IReport
{
    string Id { get; set; }

    int ReportId { get; set; }

    ReportStateEnum State { get; set; }

    DateTime? Date { get; set; }

    DateTime? ClearedDate { get; set; }

    Agency Agency { get; set; }
    IncidentType IncidentType { get; set; }
    double? Miles { get; set; }
    Location? Location { get; set; }
    string Narrative { get; set; }
    ICollection<ReportPerson> People { get; set; }
    string AuroraUserId { get; set; }
    AuroraUser User { get; set; }
    string AgencyId { get; set; }
    string IncidentTypeId { get; set; }

    MinistryOpportunityRecord MinistryOpportunity { get; set; }

    DateTime CreatedOnUtc { get; set; }

    DateTime? DeletedOnUtc { get; set; }
}
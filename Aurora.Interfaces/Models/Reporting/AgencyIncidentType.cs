namespace Aurora.Interfaces.Models.Reporting;

public class AgencyIncidentType
{
    public string AgencyId { get; set; }

    public string IncidentTypeId { get; set; }

    public Agency Agency { get; set; }

    public IncidentType IncidentType { get; set; }
}
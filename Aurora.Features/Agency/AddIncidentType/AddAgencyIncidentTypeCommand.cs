using Aurora.Interfaces.Models.Reporting;
using MediatR;

namespace Aurora.Features.Agency.AddIncidentType;

public class AddAgencyIncidentTypeCommand : IRequest<AddAgencyIncidentTypeResult>
{
    public AddAgencyIncidentTypeCommand(string agencyId, IncidentTypeRecord incidentType)
    {
        AgencyId = agencyId;
        IncidentType = incidentType;
    }

    public string? AgencyId { get; set; }

    public IncidentTypeRecord? IncidentType { get; set; }
}
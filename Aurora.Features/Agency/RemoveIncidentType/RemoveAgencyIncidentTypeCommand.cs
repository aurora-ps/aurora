using Aurora.Interfaces.Models.Reporting;
using MediatR;

namespace Aurora.Features.Agency.RemoveIncidentType;

public class RemoveAgencyIncidentTypeCommand : IRequest<RemoveAgencyIncidentTypeResult>
{
    // Agency id
    public string AgencyId { get; set; }

    // Incident type id
    public IncidentTypeRecord? IncidentTypeRecord { get; set; }
}

// create result

// handler
using Aurora.Interfaces.Models.Reporting;
using MediatR;

namespace Aurora.Features.Agency.UpdateIncidentTypeOverrides;

public class UpdateIncidentTypeOverridesCommand : IRequest<UpdateIncidentTypeOverridesResult>
{
    public string? AgencyId { get; set; }

    public IncidentTypeRecord? IncidentType { get; set; }
}
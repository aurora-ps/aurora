using Aurora.Interfaces;
using MediatR;

namespace Aurora.Features.Agency.RemoveIncidentType;

public class RemoveAgencyIncidentTypeCommandHandler : IRequestHandler<RemoveAgencyIncidentTypeCommand, RemoveAgencyIncidentTypeResult>
{
    private readonly IClusterClient _clusterClient;

    public RemoveAgencyIncidentTypeCommandHandler(IClusterClient clusterClient)
    {
        _clusterClient = clusterClient;
    }

    public async Task<RemoveAgencyIncidentTypeResult> Handle(RemoveAgencyIncidentTypeCommand command, CancellationToken cancellationToken)
    {
        // get grain
        var agencyGrain = _clusterClient.GetGrain<IAgencyGrain>(command.AgencyId);
        // not found return Not found result
        if (agencyGrain == null)
        {
            return RemoveAgencyIncidentTypeResult.NotFound();
        }

        // get agency
        var agency = await agencyGrain.GetDetailsAsync();
        // not found return Not found result
        if (agency == null)
        {
            return RemoveAgencyIncidentTypeResult.NotFound();
        }

        var success = await agencyGrain.RemoveIncidentTypeAsync(command.IncidentTypeRecord);
        await agencyGrain.SaveChangesAsync();

        return RemoveAgencyIncidentTypeResult.Success();

    }
}
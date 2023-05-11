using Aurora.Interfaces;
using MediatR;

namespace Aurora.Features.Agency.GetAgency;

public class GetAgencyHandler : IRequestHandler<GetAgencyQuery, GetAgencyResult>
{
    private readonly IClusterClient _clusterClient;

    public GetAgencyHandler(IClusterClient clusterClient)
    {
        _clusterClient = clusterClient;
    }

    public async Task<GetAgencyResult> Handle(GetAgencyQuery request, CancellationToken cancellationToken)
    {
        // validate the command
        var validator = new GetAgencyQueryValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        // if the command is not valid, return not found
        if (!validationResult.IsValid)
            return GetAgencyResult.NotFound();

        // get the agency grain
        var agencyGrain = _clusterClient.GetGrain<IAgencyGrain>(request.AgencyId);
        var agency = await agencyGrain.GetDetailsAsync();
        if (agency == null)
            return GetAgencyResult.NotFound();

        return GetAgencyResult.Success(agency);
    }
}
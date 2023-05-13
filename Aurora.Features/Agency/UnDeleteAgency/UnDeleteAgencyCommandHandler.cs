using Aurora.Interfaces;
using FluentValidation.Results;
using MediatR;

namespace Aurora.Features.Agency.UnDeleteAgency;

public class UnDeleteAgencyCommandHandler : IRequestHandler<UnDeleteAgencyCommand, UnDeleteAgencyResponse>
{
    private readonly IClusterClient _clusterClient;

    public UnDeleteAgencyCommandHandler(IClusterClient clusterClient)
    {
        _clusterClient = clusterClient;
    }

    public async Task<UnDeleteAgencyResponse> Handle(UnDeleteAgencyCommand request, CancellationToken cancellationToken)
    {
        // validate command
        var validator = new UnDeleteAgencyValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid) return UnDeleteAgencyResponse.ValidationFailure(validationResult.Errors);

        // get agency
        var agencyGrain = _clusterClient.GetGrain<IAgencyGrain>(request.AgencyId);
        var agency = await agencyGrain.GetDetailsAsync();
        if (agency == null)
            return UnDeleteAgencyResponse.ValidationFailure(new List<ValidationFailure>
            {
                new(nameof(request.AgencyId), $"Agency with id {request.AgencyId} not found")
            });

        // delete agency
        if (agency.DeletedOnUtc.HasValue)
        {
            await agencyGrain.UnDeleteAsync();
            if (await agencyGrain.SaveChangesAsync()) return UnDeleteAgencyResponse.UnDeleted();
        }
        else
        {
            // Nothing to do, already un-deleted
            return UnDeleteAgencyResponse.UnDeleted();
        }
        
        return UnDeleteAgencyResponse.ValidationFailure(new List<ValidationFailure>
        {
            new(nameof(request.AgencyId), $"Error un-deleting Agency with id {request.AgencyId}")
        });
    }
}
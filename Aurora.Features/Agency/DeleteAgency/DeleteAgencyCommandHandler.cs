using Aurora.Interfaces;
using FluentValidation.Results;
using MediatR;

namespace Aurora.Features.Agency.DeleteAgency;

public class DeleteAgencyCommandHandler : IRequestHandler<DeleteAgencyCommand, DeleteAgencyResponse>
{
    private readonly IClusterClient _clusterClient;

    public DeleteAgencyCommandHandler(IClusterClient clusterClient)
    {
        _clusterClient = clusterClient;
    }

    public async Task<DeleteAgencyResponse> Handle(DeleteAgencyCommand request, CancellationToken cancellationToken)
    {
        // validate command
        var validator = new DeleteAgencyValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid) return DeleteAgencyResponse.ValidationFailure(validationResult.Errors);

        // get agency
        var agencyGrain = _clusterClient.GetGrain<IAgencyGrain>(request.AgencyId);
        var agency = await agencyGrain.GetDetailsAsync();
        if (agency == null)
            return DeleteAgencyResponse.ValidationFailure(new List<ValidationFailure>
            {
                new(nameof(request.AgencyId), $"Agency with id {request.AgencyId} not found")
            });

        // delete agency
        await agencyGrain.DeleteAsync();
        if (await agencyGrain.SaveChangesAsync()) return DeleteAgencyResponse.Deleted();

        return DeleteAgencyResponse.ValidationFailure(new List<ValidationFailure>
        {
            new(nameof(request.AgencyId), $"Error deleting Agency with id {request.AgencyId}")
        });
    }
}
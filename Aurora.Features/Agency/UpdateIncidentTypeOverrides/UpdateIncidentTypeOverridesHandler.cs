using Aurora.Interfaces;
using FluentValidation.Results;
using MediatR;

namespace Aurora.Features.Agency.UpdateIncidentTypeOverrides;

public class
    UpdateIncidentTypeOverridesHandler : IRequestHandler<UpdateIncidentTypeOverridesCommand,
        UpdateIncidentTypeOverridesResult>
{
    private readonly IClusterClient _clusterClient;

    public UpdateIncidentTypeOverridesHandler(IClusterClient clusterClient)
    {
        _clusterClient = clusterClient;
    }

    public async Task<UpdateIncidentTypeOverridesResult> Handle(UpdateIncidentTypeOverridesCommand request,
        CancellationToken cancellationToken)
    {
        // validate the command
        var validator = new UpdateIncidentTypeOverridesCommandValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        // if the command is not valid, return the validation errors
        if (!validationResult.IsValid)
            return UpdateIncidentTypeOverridesResult.Create(validationResult.Errors);

        // get the agency grain
        var agencyGrain = _clusterClient.GetGrain<IAgencyGrain>(request.AgencyId);
        if (await agencyGrain.GetDetailsAsync() == null)
            return UpdateIncidentTypeOverridesResult.Create(new List<ValidationFailure>
                { new("Id", "Agency not found") });

        // update the incident type
        await agencyGrain.UpdateIncidentTypeAsync(request.IncidentType!);
        // save changes
        await agencyGrain.SaveChangesAsync();

        return UpdateIncidentTypeOverridesResult.CreateSuccess();
    }
}
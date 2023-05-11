using Aurora.Interfaces;
using FluentValidation.Results;
using MediatR;

namespace Aurora.Features.Agency.AddIncidentType
{
    public class AddAgencyIncidentTypeHandler : IRequestHandler<AddAgencyIncidentTypeCommand, AddAgencyIncidentTypeResult>
    {
        private readonly IClusterClient _clusterClient;

        public AddAgencyIncidentTypeHandler(IClusterClient clusterClient)
        {
            _clusterClient = clusterClient;
        }

        public async Task<AddAgencyIncidentTypeResult> Handle(AddAgencyIncidentTypeCommand command, CancellationToken cancellationToken)
        {
            // validate the command
            var validator = new AddAgencyIncidentTypeCommandValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);

            // if the command is not valid, return the validation errors
            if (!validationResult.IsValid)
                return AddAgencyIncidentTypeResult.Create(validationResult.Errors);

            // get the agency grain
            var agencyGrain = _clusterClient.GetGrain<IAgencyGrain>(command.AgencyId);
            if (await agencyGrain.GetDetailsAsync() == null)
                return AddAgencyIncidentTypeResult.Create(new List<ValidationFailure> { new("Id", "Agency not found") });

            // add the incident type
            await agencyGrain.AddIncidentTypeAsync(command.IncidentType!);
            // save changes
            await agencyGrain.SaveChangesAsync();

            return AddAgencyIncidentTypeResult.CreateSuccess();
        }
    }
}

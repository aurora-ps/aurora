using Aurora.Interfaces;
using Aurora.Interfaces.Models.Reporting;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace Aurora.Features.Agency.AddAgency
{
    public class AddAgencyCommandHandler : IRequestHandler<AddAgencyCommand, AddAgencyCommandResult>
    {
        private readonly IClusterClient _clusterClient;

        public AddAgencyCommandHandler(IClusterClient clusterClient)
        {
            _clusterClient = clusterClient;
        }

        public async Task<AddAgencyCommandResult> Handle(AddAgencyCommand command, CancellationToken cancellationToken)
        {
            // Validate command
            var validator = new AddAgencyCommandValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
            {
                return AddAgencyCommandResult.Error(validationResult.Errors);
            }

            if (await this.AgencyExists(command.Name))
            {
                return AddAgencyCommandResult.Error(new List<ValidationFailure> { new("Name", "Agency already exists") });
            }

            var agencyGrain = _clusterClient.GetGrain<IAgencyGrain>(Guid.NewGuid().ToString());
            await agencyGrain.SetAgencyName(command.Name);
            await agencyGrain.SaveChangesAsync();

            var agencyRecord = await agencyGrain.GetDetailsAsync();
            // Return error if agency record is null
            return agencyRecord == null ? AddAgencyCommandResult.Error(new List<ValidationFailure> { new("Name", "Agency could not be created") }) : AddAgencyCommandResult.Created(agencyRecord);
        }

        private async Task<bool> AgencyExists(string agencyName)
        {
            var results = await _clusterClient.GetGrain<IAgencyManagementGrain>("").GetAgenciesAsync(agencyName);
            if (results == null || results.Count == 0)
            {
                return false;
            }

            return results.Any(_ => _.Name.Trim() == agencyName.Trim());
        }
    }

    public class AddAgencyCommand : IRequest<AddAgencyCommandResult>
    {
        private AddAgencyCommand(string name)
        {
            Name = name;
        }

        public static AddAgencyCommand Create(string name)
        {
            return new AddAgencyCommand(name);
        }

        public string Name { get; }
    }

    public class AddAgencyCommandResult
    {
        private AddAgencyCommandResult(AgencyRecord agencyRecord)
        {
            AgencyRecord = agencyRecord;
            Success = true;
        }

        private AddAgencyCommandResult(List<ValidationFailure> validationResultErrors)
        {
            ValidationResultErrors = validationResultErrors;
            Success = false;
        }
        
        public AgencyRecord? AgencyRecord { get; }

        public List<ValidationFailure>? ValidationResultErrors { get; }

        public bool Success { get; }

        public static AddAgencyCommandResult Created(AgencyRecord agencyRecord) => new(agencyRecord);

        public static AddAgencyCommandResult Error(List<ValidationFailure> validationResultErrors) => new(validationResultErrors);
    }

    public class AddAgencyCommandValidator : AbstractValidator<AddAgencyCommand>
    {
        public AddAgencyCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
        }
    }
}

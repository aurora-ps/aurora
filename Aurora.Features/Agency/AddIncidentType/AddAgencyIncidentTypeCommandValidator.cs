using FluentValidation;

namespace Aurora.Features.Agency.AddIncidentType;

public class AddAgencyIncidentTypeCommandValidator : AbstractValidator<AddAgencyIncidentTypeCommand>
{
    public AddAgencyIncidentTypeCommandValidator()
    {
        // Id must not be null
        RuleFor(x => x.AgencyId).NotNull();
        // IncidentType must not be null
        RuleFor(x => x.IncidentType).NotNull();
    }
}
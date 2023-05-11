using FluentValidation;

namespace Aurora.Features.Agency.UpdateIncidentTypeOverrides;

public class UpdateIncidentTypeOverridesCommandValidator : AbstractValidator<UpdateIncidentTypeOverridesCommand>
{
    public UpdateIncidentTypeOverridesCommandValidator()
    {
        RuleFor(x => x.AgencyId).NotEmpty();
        RuleFor(x => x.IncidentType).NotNull();
    }
}
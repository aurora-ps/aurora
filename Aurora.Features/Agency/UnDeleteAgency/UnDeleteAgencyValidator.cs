using FluentValidation;

namespace Aurora.Features.Agency.UnDeleteAgency;

public class UnDeleteAgencyValidator : AbstractValidator<UnDeleteAgencyCommand>
{
    public UnDeleteAgencyValidator()
    {
        RuleFor(x => x.AgencyId).NotEmpty();
    }
}
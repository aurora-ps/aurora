using FluentValidation;

namespace Aurora.Features.Agency.GetAgency;

public class GetAgencyQueryValidator : AbstractValidator<GetAgencyQuery>
{
    public GetAgencyQueryValidator()
    {
        RuleFor(x => x.AgencyId).NotEmpty();
    }
}
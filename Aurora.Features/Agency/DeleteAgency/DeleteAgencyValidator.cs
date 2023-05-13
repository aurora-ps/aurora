using FluentValidation;

namespace Aurora.Features.Agency.DeleteAgency;

public class DeleteAgencyValidator : AbstractValidator<DeleteAgencyCommand>
{
    public DeleteAgencyValidator()
    {
        RuleFor(x => x.AgencyId).NotEmpty();
    }
}
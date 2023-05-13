using FluentValidation;

namespace Aurora.Features.Agency.AddAgency;

public class AddAgencyCommandValidator : AbstractValidator<AddAgencyCommand>
{
    public AddAgencyCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
    }
}
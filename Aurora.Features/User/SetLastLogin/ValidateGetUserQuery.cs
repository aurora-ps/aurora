using FluentValidation;

namespace Aurora.Features.User.SetLastLogin;

public class ValidateGetUserQuery : AbstractValidator<SetLastLoginCommand>
{
    public ValidateGetUserQuery()
    {
        RuleFor(_ => _.UserId).NotEmpty();
        RuleFor(_ => _.LastLogin).NotEmpty();
    }
}
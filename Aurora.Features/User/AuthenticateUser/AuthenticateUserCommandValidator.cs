using FluentValidation;

namespace Aurora.Features.User.AuthenticateUser;

public class AuthenticateUserCommandValidator : AbstractValidator<AuthenticateUserCommand>
{
    public AuthenticateUserCommandValidator()
    {
        RuleFor(x => x.UserName).NotEmpty();
        RuleFor(x => x.Password).NotEmpty();
    }
}
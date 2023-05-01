using System.Text.RegularExpressions;
using FluentValidation;

namespace Aurora.Features.User.RegisterUser;

public class RegisterUserValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserValidator()
    {
        RuleFor(x => x.UserName).NotEmpty().WithMessage("User Name is required");
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required")
            .Length(10, 40).WithMessage("Password must be between 10 and 40 characters")
            .Must(HasValidPassword).WithMessage("Password must contain at least one number, one uppercase letter")
            ;
    }

    private bool HasValidPassword(string password)
    {
        var hasNumber = new Regex(@"[0-9]+");
        var hasUpperChar = new Regex(@"[A-Z]+");

        return hasNumber.IsMatch(password) && hasUpperChar.IsMatch(password);
    }
}
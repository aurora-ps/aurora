using FluentValidation;

namespace Aurora.Api.Routers.Models;

public class AddUserModelValidator : AbstractValidator<AddUserModel>
{
    public AddUserModelValidator()
    {
        RuleFor(x => x.UserName).NotEmpty();
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
    }
}
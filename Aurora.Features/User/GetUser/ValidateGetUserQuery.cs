using FluentValidation;

namespace Aurora.Features.User.GetUser;

public class ValidateGetUserQuery : AbstractValidator<GetUserQuery>
{
    public ValidateGetUserQuery()
    {
        RuleFor(_ => _.UserId).NotEmpty();
    }
}
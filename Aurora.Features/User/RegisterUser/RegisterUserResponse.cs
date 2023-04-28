using Aurora.Interfaces;
using FluentValidation.Results;

namespace Aurora.Features.User.RegisterUser;

public class RegisterUserResponse
{
    private RegisterUserResponse(bool success)
    {
        IsSuccess = success;
    }


    public List<string>? Errors { get; set; }

    public bool IsSuccess { get; set; }

    public UserRecord User { get; set; }

    public static RegisterUserResponse Conflict()
    {
        return new RegisterUserResponse(false);
    }

    public static RegisterUserResponse CreateFailure(IList<ValidationFailure> validationResultErrors)
    {
        return new RegisterUserResponse(false)
            { Errors = validationResultErrors.Select(x => x.ErrorMessage).ToList() };
    }

    public static RegisterUserResponse Unauthorized()
    {
        return new RegisterUserResponse(false);
    }

    public static RegisterUserResponse Created(UserRecord user)
    {
        return new RegisterUserResponse(true)
        {
            User = user
        };
    }
}
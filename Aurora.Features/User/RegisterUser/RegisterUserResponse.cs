using Aurora.Interfaces;
using Aurora.Interfaces.Models;
using FluentValidation.Results;

namespace Aurora.Features.User.RegisterUser;

public class RegisterUserResponse
{
    public enum RegisterUserResponseEnum
    {
        Conflict,
        Created,
        Unauthorized,
        Error
    }

    private RegisterUserResponse(bool success, RegisterUserResponseEnum responseType)
    {
        IsSuccess = success;
        ResponseType = responseType;
    }

    public List<string>? Errors { get; set; }

    public bool IsSuccess { get; set; }

    public UserRecord User { get; set; }

    public AuroraUser IdentityUser { get; set; }

    public string Token { get; set; }

    public RegisterUserResponseEnum ResponseType { get; set; }

    public static RegisterUserResponse Conflict()
    {
        return new RegisterUserResponse(false, RegisterUserResponseEnum.Conflict);
    }

    public static RegisterUserResponse CreateFailure(IList<ValidationFailure> validationResultErrors)
    {
        return new RegisterUserResponse(false, RegisterUserResponseEnum.Error)
            { Errors = validationResultErrors.Select(x => x.ErrorMessage).ToList() };
    }

    public static RegisterUserResponse Unauthorized()
    {
        return new RegisterUserResponse(false, RegisterUserResponseEnum.Unauthorized);
    }

    public static RegisterUserResponse Created(UserRecord user)
    {
        return new RegisterUserResponse(true, RegisterUserResponseEnum.Created)
        {
            User = user
        };
    }
}
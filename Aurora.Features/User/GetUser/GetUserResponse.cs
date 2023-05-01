using Aurora.Interfaces;
using FluentValidation.Results;

namespace Aurora.Features.User.GetUser;

public class GetUserResponse : BaseResponse
{
    public UserRecord User { get; set; }

    public List<ValidationFailure> ValidationErrors { get; set; }

    public static GetUserResponse CreateSuccess(UserRecord user)
    {
        var response = new GetUserResponse
        {
            Success = true,
            User = user
        };

        return response;
    }

    public static GetUserResponse CreateFailure(List<ValidationFailure> validationResultErrors)
    {
        return new GetUserResponse
        {
            Success = false,
            ValidationErrors = validationResultErrors
        };
    }
}
using Aurora.Interfaces;

namespace Aurora.Features.User;

public class GetUserResponse : BaseResponse
{
    public UserRecord User { get; set; }

    public static GetUserResponse CreateSuccess(UserRecord user)
    {
        var response = new GetUserResponse
        {
            Success = true,
            User = user
        };

        return response;
    }
}

public abstract class BaseResponse
{
    public bool Success { get; set; }
}
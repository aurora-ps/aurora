using Aurora.Interfaces;

namespace Aurora.Api.Endpoints.User;

public class GetUserResponse : BaseResponse
{

    public static GetUserResponse CreateSuccess(UserRecord user)
    {
        var response = new GetUserResponse
        {
            Success = true,
            User = user
        };

        return response;
    }

    public UserRecord User { get; set; }
}

public abstract class BaseResponse
{
    public bool Success { get; set; }
}
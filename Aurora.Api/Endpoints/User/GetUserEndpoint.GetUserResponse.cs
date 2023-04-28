using Aurora.Interfaces;

namespace Aurora.Api.Endpoints.User;

public class GetUserResponse
{
    public bool Success { get; set; }

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
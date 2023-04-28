using Aurora.Interfaces;

namespace Aurora.Api.Endpoints.User;

public class AddUserResponse : BaseResponse
{
    public static AddUserResponse CreateSuccess(UserRecord userRecord)
    {
        return new()
        {
            Success = true,
            User = userRecord
        };
    }
    public UserRecord? User { get; set; }

    public static AddUserResponse CreateFailure()
    {
        return new AddUserResponse
        {
            Success = false
        };
    }
}
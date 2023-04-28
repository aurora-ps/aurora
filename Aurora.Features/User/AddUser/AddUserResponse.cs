using Aurora.Interfaces;

namespace Aurora.Features.User.AddUser;

public class AddUserResponse : BaseResponse
{
    public UserRecord? User { get; set; }

    public static AddUserResponse CreateSuccess(UserRecord userRecord)
    {
        return new AddUserResponse
        {
            Success = true,
            User = userRecord
        };
    }

    public static AddUserResponse CreateFailure()
    {
        return new AddUserResponse
        {
            Success = false
        };
    }
}
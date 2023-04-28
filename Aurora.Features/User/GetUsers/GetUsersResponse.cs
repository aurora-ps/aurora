using Aurora.Interfaces;

namespace Aurora.Features.User.GetUsers;

public class GetUsersResponse
{
    public bool Success { get; set; }
    public IList<UserRecord> Users { get; set; }

    public static GetUsersResponse CreateSuccess(IList<UserRecord> users)
    {
        return new GetUsersResponse
        {
            Success = true,
            Users = users
        };
    }
}
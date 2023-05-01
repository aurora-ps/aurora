namespace Aurora.Features.User.Logout;

public class LogoutUserResponse
{
    private LogoutUserResponse(bool success)
    {
        Success = success;
    }

    public bool Success { get; set; }

    public static LogoutUserResponse CreateSuccess()
    {
        return new LogoutUserResponse(true);
    }
}
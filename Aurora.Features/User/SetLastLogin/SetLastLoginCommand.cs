using MediatR;

namespace Aurora.Features.User.SetLastLogin;

public class SetLastLoginCommand : IRequest
{
    private SetLastLoginCommand(string userId)
    {
        UserId = userId;
        LastLogin = DateTime.UtcNow;
    }

    public string UserId { get; init; }

    public DateTime LastLogin { get; init; }

    public static SetLastLoginCommand Create(string userId)
    {
        return new SetLastLoginCommand(userId);
    }
}
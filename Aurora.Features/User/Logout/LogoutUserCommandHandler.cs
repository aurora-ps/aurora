using Aurora.Interfaces.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Aurora.Features.User.Logout;

public class LogoutUserCommandHandler : IRequestHandler<LogoutUserCommand, LogoutUserResponse>
{
    private readonly UserManager<AuroraUser> _userManager;

    public LogoutUserCommandHandler(UserManager<AuroraUser> userManager)
    {
        _userManager = userManager;
    }

    public Task<LogoutUserResponse> Handle(LogoutUserCommand request, CancellationToken cancellationToken)
    {
        return Task.FromResult(LogoutUserResponse.CreateSuccess());
    }
}

public class LogoutUserCommand : IRequest<LogoutUserResponse>
{
}

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
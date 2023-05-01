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
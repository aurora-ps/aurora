using MediatR;

namespace Aurora.Features.User.Logout;

public class LogoutUserCommand : IRequest<LogoutUserResponse>
{
}
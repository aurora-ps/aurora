using MediatR;

namespace Aurora.Features.User.DeleteUser;

public class DeleteUserCommand : IRequest<DeleteUserResponse>
{
    public string UserId { get; set; }
}
using MediatR;

namespace Aurora.Features.User.GetUser;

public class GetUserQuery : IRequest<GetUserResponse>
{
    public string UserId { get; set; }
}
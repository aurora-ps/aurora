using MediatR;

namespace Aurora.Features.User;

public class GetUserQuery : IRequest<GetUserResponse>
{
    public string UserId { get; set; }
}
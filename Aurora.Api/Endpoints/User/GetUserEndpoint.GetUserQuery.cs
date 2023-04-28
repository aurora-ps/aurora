using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Aurora.Api.Endpoints.User;

public class GetUserQuery : IRequest<GetUserResponse>
{
    [FromRoute]
    public string UserId { get; set; }
}
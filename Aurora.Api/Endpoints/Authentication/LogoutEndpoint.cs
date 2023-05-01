using Aurora.Features.User.Logout;
using MediatR;

namespace Aurora.Api.Endpoints.Authentication;

public class LogoutEndpoint : AuthenticationRouteBase
{
    public const string Route = $"/{UrlFragment}/logout/{{userId}}";

    public static async Task<IResult> Logout(IMediator mediator, [AsParameters] LogoutUserCommand logoutCommand)
    {
        var result = await mediator.Send(logoutCommand);
        if (result is null || !result.Success)
            return TypedResults.BadRequest();
        return TypedResults.Ok(result);
    }
}
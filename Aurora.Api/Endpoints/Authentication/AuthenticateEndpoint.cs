using Aurora.Features.User.AuthenticateUser;
using Aurora.Features.User.Logout;
using MediatR;

namespace Aurora.Api.Endpoints.Authentication;

public class AuthenticateEndpoint : AuthenticationRouteBase
{
    public const string Route = $"/{UrlFragment}/login";

    public static async Task<IResult> Authenticate(IMediator mediator, AuthenticateUserCommand command)
    {
        var result = await mediator.Send(command);
        if (result is null || !result.Success)
            return TypedResults.Unauthorized();
        return TypedResults.Ok(result);
    }
}

public class LogoutEndpoint : AuthenticationRouteBase
{
    public const string Route = $"/{UrlFragment}/logout/{{userId}}";

    public static async Task<IResult> Logout(IMediator mediator, [AsParameters]LogoutUserCommand logoutCommand)
    {
        var result = await mediator.Send(logoutCommand);
        if (result is null || !result.Success)
            return TypedResults.BadRequest();
        return TypedResults.Ok(result);
    }

}
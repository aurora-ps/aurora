using Aurora.Features.User.AuthenticateUser;
using MediatR;

namespace Aurora.Api.Endpoints.Authentication;

public class AuthenticateEndpoint : AuthenticationRouteBase
{
    public const string Route = $"/{UrlFragment}/login";

    public static async Task<IResult> Authenticate(HttpContext httpContext, IMediator mediator,
        AuthenticateUserCommand command)
    {
        var result = await mediator.Send(command);
        if (result is null || !result.Success)
            return TypedResults.Unauthorized();

        httpContext.Response.Headers.Add("Authorization", $"Bearer {result.Token}");

        return TypedResults.Ok(result);
    }
}
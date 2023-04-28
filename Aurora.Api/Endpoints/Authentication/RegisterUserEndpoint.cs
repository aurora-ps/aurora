using Aurora.Features.User.RegisterUser;
using MediatR;

namespace Aurora.Api.Endpoints.Authentication;

public class RegisterUserEndpoint : AuthenticateEndpoint
{
    public const string Route = $"/{UrlFragment}/register";

    public static async Task<IResult> Register(IMediator mediator, RegisterUserCommand command)
    {
        var result = await mediator.Send(command);
        if (result is null || !result.IsSuccess)
            return TypedResults.Unauthorized();
        return TypedResults.Ok(result.User);
    }
}
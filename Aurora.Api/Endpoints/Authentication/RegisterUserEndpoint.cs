using Aurora.Features.User.RegisterUser;
using MediatR;

namespace Aurora.Api.Endpoints.Authentication;

public class RegisterUserEndpoint : AuthenticateEndpoint
{
    public const string Route = $"/{UrlFragment}/register";

    public static async Task<IResult> Register(IMediator mediator, RegisterUserCommand command)
    {
        var result = await mediator.Send(command);
        switch (result.ResponseType)
        {
            case RegisterUserResponse.RegisterUserResponseEnum.Conflict:
                return TypedResults.Conflict();
            case RegisterUserResponse.RegisterUserResponseEnum.Created:
                return TypedResults.Created(result.User.Id);
            case RegisterUserResponse.RegisterUserResponseEnum.Error:
                return TypedResults.BadRequest(result.Errors);
            case RegisterUserResponse.RegisterUserResponseEnum.Unauthorized:
                return TypedResults.Unauthorized();
            default:
                return TypedResults.Problem();
        }
    }
}
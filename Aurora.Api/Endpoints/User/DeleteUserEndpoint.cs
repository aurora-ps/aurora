using Aurora.Features.User.DeleteUser;
using MediatR;

namespace Aurora.Api.Endpoints.User;

public class DeleteUserEndpoint : UserRouteBase
{
    public const string Route = $"/{UrlFragment}/{{userId}}";

    public static async Task<IResult> DeleteUser(IMediator mediator, [AsParameters] DeleteUserCommand command)
    {
        var result = await mediator.Send(command);

        switch (result.Status)
        {
            case DeleteUserResponse.DeleteUserStatusEnum.Deleted:
                return TypedResults.Ok();
            case DeleteUserResponse.DeleteUserStatusEnum.NotFound:
                return TypedResults.NotFound();
            case DeleteUserResponse.DeleteUserStatusEnum.Error:
                return TypedResults.Problem();
            case DeleteUserResponse.DeleteUserStatusEnum.Unauthorized:
                return TypedResults.Unauthorized();
        }

        return TypedResults.Problem();
    }
}
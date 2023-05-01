using Aurora.Features.User.GetUsers;
using MediatR;

namespace Aurora.Api.Endpoints.User;

public class GetUsersEndpoint : UserRouteBase
{
    public const string Route = $"/{UrlFragment}";

    public static async Task<IResult> GetUsers(IMediator mediator)
    {
        var results = await mediator.Send(new GetUsersQuery());
        if (!results.Success)
            return Results.NotFound();

        return TypedResults.Ok(results.Users);
    }
}
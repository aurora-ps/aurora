using MediatR;

namespace Aurora.Api.Endpoints.User;

public class GetUserEndpoint : UserRouteBase
{
    public const string Route = $"/{UrlFragment}/{{userId}}";

    public static async Task<IResult> GetUser(IMediator mediatr, string userId)
    {
        var userQuery = new GetUserQuery { UserId = userId };
        var user = await mediatr.Send(userQuery);
        
        if (user is null || !user.Success)
            return TypedResults.NotFound();

        return TypedResults.Ok(user.User);
    }
}
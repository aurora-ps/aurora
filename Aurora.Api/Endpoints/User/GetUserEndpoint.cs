using Aurora.Features.User;
using Aurora.Features.User.GetUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Aurora.Api.Endpoints.User;

public class GetUserEndpoint : UserRouteBase
{
    public const string Route = $"/{UrlFragment}/{{userId}}";

    public static async Task<IResult> GetUser(IMediator mediatr, [AsParameters] GetUserQuery userQuery)
    {
        var user = await mediatr.Send(userQuery);

        if (user is null || !user.Success)
            return TypedResults.NotFound();

        return TypedResults.Ok(user.User);
    }
}
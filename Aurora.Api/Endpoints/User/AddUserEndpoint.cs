using MediatR;
using Microsoft.AspNetCore.Mvc;
using Orleans;

namespace Aurora.Api.Endpoints.User;

public class AddUserEndpoint : UserRouteBase
{
    public const string Route = $"/{UrlFragment}";

    public static async Task<IResult> AddUser(IMediator mediator,
        [FromBody] AddUserCommand user)
    {
        var results = await mediator.Send(user);
        if(!results.Success)
            return Results.NotFound();

        return Results.Ok(results.User);
    }
}
using Aurora.Api.Routers.Models;
using Aurora.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Aurora.Api.Endpoints.User;

public class AddUserEndpoint : UserRouteBase
{
    public const string Route = $"/{UrlFragment}";

    public static async Task<IResult> AddUser([FromServices] IClusterClient clusterClient,
        [FromBody] AddUserModel user)
    {
        var grain = clusterClient.GetGrain<IUserGrain>(Guid.NewGuid().ToString());
        var userRecord = await grain.AddAsync(user.UserName, user.Email);
        if (userRecord is null)
            return TypedResults.NotFound();

        return TypedResults.Ok(userRecord);
    }
}
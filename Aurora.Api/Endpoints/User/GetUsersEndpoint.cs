using Aurora.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Aurora.Api.Endpoints.User;

public class GetUsersEndpoint : UserRouteBase
{
    public const string Route = $"/{UrlFragment}";

    public static async Task<IResult> GetUsers([FromServices] IClusterClient clusterClient)
    {
        var userService = clusterClient.GetGrain<IUserServiceGrain>("");
        var users = await userService.GetAllAsync();
        return TypedResults.Ok(users);
    }
}
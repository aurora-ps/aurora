using Aurora.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Aurora.Api.Routers;

public static class UserRouterGroups
{
    public static RouteGroupBuilder UserRoutes(this RouteGroupBuilder group)
    {
        group.MapGet("/user", GetUsers);
        group.MapGet("/user/{userId}", GetUser);
        group.MapPut("/user", AddUser);
        return group.WithOpenApi();
    }

    private static async Task<IResult> GetUsers([FromServices] IClusterClient clusterClient)
    {
        var client = clusterClient.GetGrain<IUserServiceGrain>("");
        return TypedResults.Ok(await client.GetUsersAsync());
    }

    private static async Task<IResult> GetUser([FromServices] IClusterClient clusterClient, string userId)
    {
        var grain = clusterClient.GetGrain<IUserGrain>(userId);
        var user = await grain.GetDetailsAsync();
        if (user is null)
            return TypedResults.NotFound();

        return TypedResults.Ok(user);
    }

    private static async Task<IResult> AddUser([FromServices] IClusterClient clusterClient, [FromBody] AddUserDto user)
    {
        var grain = clusterClient.GetGrain<IUserGrain>(Guid.NewGuid().ToString());
        var userRecord = await grain.AddAsync(user.UserName, user.Email);
        if (userRecord is null)
            return TypedResults.NotFound();

        return TypedResults.Ok(userRecord);
    }



    [Serializable]
    private record AddUserDto(string UserName, string Email);
}
using Aurora.Api.Routers.Models;
using Aurora.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Aurora.Api.Routers;

public static class UserRouterGroups
{
    private const string UrlFragment = "user";

    public static RouteGroupBuilder UserRoutes(this RouteGroupBuilder group)
    {
        group.MapGet($"/{UrlFragment}", GetUsers);
        group.MapGet($"/{UrlFragment}/{{userId}}", GetUser);
        group.MapPost($"/{UrlFragment}", AddUser);
        group.MapDelete($"/{UrlFragment}/{{userId}}", DeleteUser);
        return group.WithOpenApi();
    }

    private static async Task<IResult> GetUsers([FromServices] IClusterClient clusterClient)
    {
        var userService = clusterClient.GetGrain<IUserServiceGrain>("");
        var users = await userService.GetAllAsync();
        return TypedResults.Ok(users);
    }

    private static async Task<IResult> GetUser([FromServices] IClusterClient clusterClient, string userId)
    {
        var grain = clusterClient.GetGrain<IUserGrain>(userId);
        var user = await grain.GetDetailsAsync();
        if (user is null)
            return TypedResults.NotFound();

        return TypedResults.Ok(user);
    }

    private static async Task<IResult> DeleteUser(IClusterClient clusterClient, UserManager<IdentityUser> userManager, string userId)
    {
        var grain = clusterClient.GetGrain<IUserGrain>(userId);
        var user = await grain.GetDetailsAsync();
        if (user is null)
            return TypedResults.NotFound();

        var identityUser = await userManager.FindByIdAsync(user.Id);
        if(identityUser is null)
            return TypedResults.NotFound();

        await userManager.DeleteAsync(identityUser);   

        return TypedResults.Ok();
    }

    private static async Task<IResult> AddUser([FromServices] IClusterClient clusterClient, [FromBody] AddUserModel user)
    {
        var grain = clusterClient.GetGrain<IUserGrain>(Guid.NewGuid().ToString());
        var userRecord = await grain.AddAsync(user.UserName, user.Email);
        if (userRecord is null)
            return TypedResults.NotFound();

        return TypedResults.Ok(userRecord);
    }
}
//using Aurora.Api.Data.Models;
//using Aurora.Api.Routers.Models;
//using Aurora.Interfaces;
//using Aurora.Interfaces.Models;
//using MediatR;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;

//namespace Aurora.Api.Endpoints.User;

//public static class UserRoutes
//{
//    private const string UrlFragment = "user";

//    public static RouteGroupBuilder ConfigureEndpoints(this RouteGroupBuilder group)
//    {
//        //group.MapGet($"/{UrlFragment}", GetUsers);
//        //group.MapGet($"/{UrlFragment}/{{userId}}", GetUser);
//        //group.MapPost($"/{UrlFragment}", AddUser);
//        group.MapDelete($"/{UrlFragment}/{{userId}}", DeleteUser);
//        return group.WithOpenApi();
//    }

//    private static async Task<IResult> GetUsers([FromServices] IClusterClient clusterClient)
//    {
//        var userService = clusterClient.GetGrain<IUserServiceGrain>("");
//        var users = await userService.GetAllAsync();
//        return TypedResults.Ok(users);
//    }

//    private static async Task<IResult> GetUser(IMediator mediatr, string userId)
//    {
//        var userQuery = new GetUserQuery { UserId = userId };
//        var user = await mediatr.Send(userQuery);
        
//        if (user is null || !user.Success)
//            return TypedResults.NotFound();

//        return TypedResults.Ok(user.User);
//    }

//    private static async Task<IResult> DeleteUser(IClusterClient clusterClient, UserManager<AuroraUser> userManager, string userId)
//    {
//        var grain = clusterClient.GetGrain<IUserGrain>(userId);
//        var user = await grain.GetDetailsAsync();
//        if (user is null)
//            return TypedResults.NotFound();

//        var identityUser = await userManager.FindByIdAsync(user.Id);
//        if (identityUser is null)
//            return TypedResults.NotFound();

//        await userManager.DeleteAsync(identityUser);

//        return TypedResults.Ok();
//    }

//    private static async Task<IResult> AddUser([FromServices] IClusterClient clusterClient, [FromBody] AddUserModel user)
//    {
//        var grain = clusterClient.GetGrain<IUserGrain>(Guid.NewGuid().ToString());
//        var userRecord = await grain.AddAsync(user.UserName, user.Email);
//        if (userRecord is null)
//            return TypedResults.NotFound();

//        return TypedResults.Ok(userRecord);
//    }
//}
using Aurora.Interfaces;
using Aurora.Interfaces.Models;
using Microsoft.AspNetCore.Identity;

namespace Aurora.Api.Endpoints.User;

public class DeleteUserEndpoint : UserRouteBase
{
    public const string Route = $"/{UrlFragment}/{{userId}}";

    public static async Task<IResult> DeleteUser(IClusterClient clusterClient, UserManager<AuroraUser> userManager,
        string userId)
    {
        var grain = clusterClient.GetGrain<IUserGrain>(userId);
        var user = await grain.GetDetailsAsync();
        if (user is null)
            return TypedResults.NotFound();

        var identityUser = await userManager.FindByIdAsync(user.Id);
        if (identityUser is null)
            return TypedResults.NotFound();

        await userManager.DeleteAsync(identityUser);

        return TypedResults.Ok();
    }
}
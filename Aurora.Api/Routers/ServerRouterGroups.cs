using Microsoft.AspNetCore.Mvc;

namespace Aurora.Api.Routers;

public static class ServerRouterGroups
{
    private static string UrlFragment { get; } = "";

    public static RouteGroupBuilder ServerRoutes(this RouteGroupBuilder group)
    {
        group.MapGet($"/{UrlFragment}", GetServer);
        return group.WithOpenApi();
    }


    private static Task<IResult> GetServer([FromServices] IClusterClient clusterClient)
    {
        return Task.FromResult<IResult>(TypedResults.Ok());
    }
}
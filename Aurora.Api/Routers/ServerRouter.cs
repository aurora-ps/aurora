﻿using Aurora.Interfaces;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Mvc;

namespace Aurora.Api.Routers;

public interface IRouterBase{}
public static class ServerRouterGroups
{
    public static RouteGroupBuilder ServerRoutes(this RouteGroupBuilder group)
    {
        group.MapGet($"/{UrlFragment}", GetServer);
        return group;
    }

    private static string UrlFragment { get; } = "";


    private static async Task<ServerState> GetServer([FromServices] IClusterClient clusterClient)
    {
        var grain = clusterClient.GetGrain<IServerGrain>("");
        return await grain.GetState();
    }
}
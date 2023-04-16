using Aurora.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Aurora.Api.Routers;

public static class OrganizationRouterGroups
{
    private const string UrlFragment = "organization";

    public static RouteGroupBuilder OrganizationRoutes(this RouteGroupBuilder group)
    {
        group.MapGet($"/{UrlFragment}", GetOrganization);
        group.MapPut($"/{UrlFragment}", AddOrganization);
        return group;
    }

    private static Task AddOrganization([FromServices] IClusterClient clusterClient, [FromQuery] string organizationName)
    {
        var grain = clusterClient.GetGrain<IOrganizationGrain>(Guid.NewGuid().ToString());
        return grain.AddAsync(organizationName);
    }

    private static async Task<OrganizationRecord?> GetOrganization([FromServices] IClusterClient clusterClient,
        string organizationId)
    {
        var grain = clusterClient.GetGrain<IOrganizationGrain>(organizationId);
        return await grain.GetDetailsAsync();
    }
}
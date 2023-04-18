using Aurora.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Aurora.Api.Routers;

public static class OrganizationRouterGroups
{
    private const string UrlFragment = "organization";

    public static RouteGroupBuilder OrganizationRoutes(this RouteGroupBuilder group)
    {
        group.MapGet($"/{UrlFragment}", GetOrganizations);
        group.MapGet($"/{UrlFragment}/{{organizationId}}", GetOrganization);
        group.MapPut($"/{UrlFragment}", AddOrganization);
        return group.WithOpenApi();
    }

    private static async Task<IResult> AddOrganization([FromServices] IClusterClient clusterClient,
        [FromQuery] string organizationName)
    {
        var grain = clusterClient.GetGrain<IOrganizationManagementGrain>("");
        var addResult = await grain.AddAsync(organizationName);
        return addResult is null ? TypedResults.NotFound() : TypedResults.Ok(addResult);
    }

    private static async Task<IResult> GetOrganization([FromServices] IClusterClient clusterClient,
        string organizationId)
    {
        var grain = clusterClient.GetGrain<IOrganizationManagementGrain>("");
        var record = await grain.GetOrganizationAsync(organizationId);
        return record is null ? TypedResults.NotFound() : TypedResults.Ok(record);
    }

    private static async Task<IResult> GetOrganizations([FromServices] IClusterClient clusterClient)
    {
        var grain = clusterClient.GetGrain<IOrganizationManagementGrain>("");
        return TypedResults.Ok(await grain.GetOrganizationsAsync());
    }
}
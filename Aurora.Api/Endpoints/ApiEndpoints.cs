using Aurora.Api.Endpoints.Authentication;

namespace Aurora.Api.Endpoints;

public static class ApiEndpoints
{
    public static RouteGroupBuilder AddApiEndpoints(this RouteGroupBuilder group)
    {
        group.ConfigureUserEndpoints();
        group.ConfigureAuthenticationEndpoints();
        return group;
    }
}
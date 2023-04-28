using Aurora.Api.Endpoints.Authentication;

namespace Aurora.Api.Endpoints;

public static class AuthenticationEndpoints
{
    public static RouteGroupBuilder ConfigureAuthenticationEndpoints(this RouteGroupBuilder group)
    {
        group.MapPost(AuthenticateEndpoint.Route, AuthenticateEndpoint.Authenticate);
        group.MapPost(LogoutEndpoint.Route, LogoutEndpoint.Logout);
        //group.MapPost(RefreshTokenEndpoint.Route, RefreshTokenEndpoint.RefreshToken);
        return group.WithOpenApi();
    }
}
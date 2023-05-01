namespace Aurora.Api.Endpoints.Authentication;

public static class AuthenticationEndpointRoutes
{
    public static RouteGroupBuilder ConfigureAuthenticationEndpoints(this RouteGroupBuilder group)
    {
        group.MapPost(AuthenticateEndpoint.Route, AuthenticateEndpoint.Authenticate);
        group.MapPost(LogoutEndpoint.Route, LogoutEndpoint.Logout);
        group.MapPost(RegisterUserEndpoint.Route, RegisterUserEndpoint.Register);
        //group.MapPost(RefreshTokenEndpoint.Route, RefreshTokenEndpoint.RefreshToken);
        return group.WithOpenApi();
    }
}
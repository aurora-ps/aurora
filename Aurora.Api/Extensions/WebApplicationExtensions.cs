using Aurora.Api.Endpoints;
using Aurora.Api.Endpoints.Authentication;
using Aurora.Api.Routers;

namespace Aurora.Api.Extensions;

public static class WebApplicationExtensions
{
    public static void ConfigureRoutes(this WebApplication app)
    {
        app.MapGroup("").ServerRoutes();
        app.MapGroup("").OrganizationRoutes();
        app.MapGroup("").ConfigureUserEndpoints();
        app.MapGroup("").AuthRoutes();
        app.MapGroup("").ConfigureAuthenticationEndpoints();
    }
}
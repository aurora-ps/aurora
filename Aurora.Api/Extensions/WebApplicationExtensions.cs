using Aurora.Api.Endpoints;

namespace Aurora.Api.Extensions;

public static class WebApplicationExtensions
{
    public static void ConfigureRoutes(this WebApplication app)
    {
        app.MapGroup("").AddApiEndpoints();
    }
}
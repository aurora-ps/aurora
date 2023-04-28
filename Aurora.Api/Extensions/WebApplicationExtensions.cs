﻿using Aurora.Api.Routers;

namespace Aurora.Api.Extensions;

public static class WebApplicationExtensions
{
    public static void ConfigureRoutes(this WebApplication app)
    {
        app.MapGroup("").ServerRoutes();
        app.MapGroup("").OrganizationRoutes();
        app.MapGroup("").UserRoutes();
        app.MapGroup("").AuthRoutes();
    }
}
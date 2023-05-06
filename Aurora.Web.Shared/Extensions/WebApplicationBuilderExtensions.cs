using System.Security.Claims;
using System.Text;
using Aurora.Features.User.AuthenticateUser;
using Aurora.Features.User.GetUser;
using Aurora.Grains.Services;
using Aurora.Infrastructure.Data;
using Aurora.Interfaces.Models;
using FluentValidation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Aurora.Web.Shared.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static void SetupAuthentication(this WebApplicationBuilder builder)
    {
        var configuration = builder.Configuration;
// Adding Authentication
        builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })

// Adding Jwt Bearer
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    //ValidAudience = configuration["JWT:ValidAudience"],
                    ValidIssuer = configuration["JWT:ValidIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
                };
            });

        builder.Services.AddAuthorization();
    }

    public static void SetupAuthenticationWithCookie(this WebApplicationBuilder builder)
    {
        var configuration = builder.Configuration;
// Adding Authentication
        builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })

// Adding Jwt Bearer
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    //ValidAudience = configuration["JWT:ValidAudience"],
                    ValidIssuer = configuration["JWT:ValidIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
                };
                options.Events = new JwtBearerEvents
                {
                    OnTokenValidated = async ctx =>
                    {
                        // Issue cookie upon successful authentication
                        var claimsIdentity = (ClaimsIdentity)ctx.Principal.Identity;
                        var claims = claimsIdentity.Claims.ToList();
                        //claims.Add(new Claim(ClaimTypes.Name, ctx.Principal.Identity.Name));
                        //claims.Add(new Claim(ClaimTypes.NameIdentifier, ctx.Principal.FindFirstValue(ClaimTypes.NameIdentifier)));
                        var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(claims, ctx.Principal.Identity.AuthenticationType));
                        await ctx.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);
                    }
                };
            })
            .AddCookie(options =>
            {
                options.Cookie.Name = ".Aurora.Cookie";
                options.LoginPath = "/Login";
                options.LogoutPath = "/Logout";
                options.AccessDeniedPath = "/AccessDenied";
                options.Cookie.HttpOnly = true;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                options.Cookie.SameSite = SameSiteMode.Strict;
                options.SlidingExpiration = true;
            });

        builder.Services.AddAuthorization();
    }

    public static void ConfigureDatabase(this WebApplicationBuilder builder)
    {
        var configuration = builder.Configuration;

        var connectionString = configuration.GetConnectionString("Aurora");
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));

        builder.Services.AddDbContext<ReportDbContext>(options =>options.UseSqlServer(connectionString, x => x.MigrationsAssembly("Aurora.Infrastructure")));

        builder.Services.AddScoped<ApplicationDbContext, ApplicationDbContext>();
        builder.Services.AddScoped<ReportDbContext, ReportDbContext>();

    }

    public static void SetupDependencies(this WebApplicationBuilder builder)
    {
        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetUserQueryHandler).Assembly));

        builder.Services.AddScoped<IOrganizationDataService, OrganizationDataService>();
        builder.Services.AddScoped<IUserDataService, UserDataService>();
        builder.Services.AddScoped<IReportDataService, ReportDataService>();

        builder.Services.AddValidatorsFromAssemblyContaining<AuthenticateUserCommandValidator>();
    }
}
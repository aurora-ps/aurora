using Aurora.Frontend.Data;
using Aurora.Frontend.Services;
using Aurora.Infrastructure.Data;
using Aurora.Interfaces.Mapping;
using Aurora.Interfaces.Models;
using Aurora.Web.Shared.Extensions;
using BlazorApp2.Areas.Identity;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureDatabase();


builder.Services
    .AddIdentity<AuroraUser, AuroraIdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders()
    .AddDefaultUI();
//builder.SetupAuthenticationWithCookie();

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddMudServices();

builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.SetupDependencies();

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<DataSeeding>();
builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<AuroraUser>>();

// Setup DI for Aurora.Infrastructure.Data
builder.Services.AddScoped<IReportDbContext, ReportDbContext>();
builder.Services.AddScoped<IApplicationDbContext, ApplicationDbContext>();

builder.Services.AddScoped<HttpContextAccessor>();
builder.Services.AddScoped<AuthenticationService>();

if (builder.Environment.IsDevelopment())
{
    builder.Host.UseOrleans((context, siloBuilder) =>
    {
        siloBuilder.AddOrleansSilo(11111, 30000);
        siloBuilder.UseDashboard();
    });
    builder.Services.AddDatabaseDeveloperPageExceptionFilter();
}
else
    //TODO: Add production configuration
{
    builder.Host.UseOrleans((context, siloBuilder) => { siloBuilder.AddOrleansSilo(11111, 30000); });
}

var app = builder.Build();

await SeedData(app);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();

async Task SeedData(WebApplication webApplication)
{
    var services = webApplication.Services.CreateScope();

    RunMigrations(services);

    void RunMigrations(IServiceScope serviceScope)
    {
        serviceScope.ServiceProvider.GetService<ApplicationDbContext>()?.Database.Migrate();
        serviceScope.ServiceProvider.GetService<ReportDbContext>()?.Database.Migrate();
    }

    var service = services.ServiceProvider.GetService<DataSeeding>();
    if (service != null)
        await service.SeedData();
}
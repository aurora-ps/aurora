using Aurora.Frontend.Data;
using Aurora.Frontend.Services;
using Aurora.Web.Shared.Extensions;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureDatabase();
builder.SetupAuthenticationWithCookie();

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddMudServices();

builder.SetupDependencies();

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<DataSeeding>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddScoped<CustomAuthenticationStateProvider>();
builder.Services.AddSingleton<AuthenticationService>();

if (builder.Environment.IsDevelopment())
    builder.Host.UseOrleans((context, siloBuilder) => { siloBuilder.AddOrleansSilo(11111, 30000); });
else
    //TODO: Add production configuration
    builder.Host.UseOrleans((context, siloBuilder) => { siloBuilder.AddOrleansSilo(11111, 30000); });

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
    var service = services.ServiceProvider.GetService<DataSeeding>();
    if(service != null)
        await service.SeedData();
}
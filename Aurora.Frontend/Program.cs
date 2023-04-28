using Aurora.Web.Shared.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureDatabase();

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.SetupDependencies();

if (builder.Environment.IsDevelopment())
    builder.Host.UseOrleans((context, siloBuilder) => { siloBuilder.AddOrleansSilo(11111, 30000); });
else
    //TODO: Add production configuration
    builder.Host.UseOrleans((context, siloBuilder) => { siloBuilder.AddOrleansSilo(11111, 30000); });


var app = builder.Build();

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

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
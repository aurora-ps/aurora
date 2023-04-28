using Aurora.Api.Extensions;
using Aurora.Web.Shared.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.ConfigureDatabase();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.SetupDependencies();

if (builder.Environment.IsDevelopment())
    builder.Host.UseOrleans((context, siloBuilder) => { siloBuilder.AddOrleansSilo(11111, 30000); });
else
    //TODO: Add production configuration
    builder.Host.UseOrleans((context, siloBuilder) => { siloBuilder.AddOrleansSilo(11111, 30000); });

var app = builder.Build();

// Configure the HTTP routes.
app.ConfigureRoutes();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();

app.Run();
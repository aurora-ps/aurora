using Aurora.Api.Extensions;
using Aurora.Api.Routers.Models;
using Aurora.Features.User.AuthenticateUser;
using Aurora.Features.User.GetUser;
using Aurora.Grains.Services;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.ConfigureDatabase();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetUserQueryHandler).Assembly));

builder.Services.AddScoped<IOrganizationDataService, OrganizationDataService>();
builder.Services.AddScoped<IUserDataService, UserDataDataService>();

builder.Services.AddValidatorsFromAssemblyContaining<AddUserModelValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<AuthenticateUserCommandValidator>();

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
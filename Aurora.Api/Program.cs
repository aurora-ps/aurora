using Aurora.Api.Routers;
using Aurora.Data.Interfaces;
using Aurora.Grains.Services;
using Aurora.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IDataService<UserRecord, string>, UserDataService>();
builder.Services.AddScoped<IOrganizationDataService, OrganizationDataService>();

builder.Host.UseOrleansClient((context, clientBuilder) =>
{
    
    if (context.HostingEnvironment.IsDevelopment())
        ConfigureOrleansForLocal(clientBuilder);
    else
        ConfigureOrleansForLocal(clientBuilder);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

SetupRoutes(app);

void SetupRoutes(WebApplication webApplication)
{
    webApplication.MapGroup("").ServerRoutes();
    webApplication.MapGroup("").OrganizationRoutes();
    webApplication.MapGroup("").UserRoutes();
}

app.Run();

void ConfigureOrleansForLocal(IClientBuilder clientBuilder)
{
    clientBuilder.UseLocalhostClustering(gatewayPort:30000);
}
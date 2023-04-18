using Aurora.Api.Routers;
using Aurora.Api.Startup;
using Aurora.Data.Interfaces;
using Aurora.Grains;
using Aurora.Grains.Services;
using Aurora.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IDataService<UserRecord, string>, UserDataService>();
builder.Services.AddScoped<IOrganizationDataService, OrganizationDataService>();

builder.Host.UseOrleans((context, siloBuilder) =>
{
    if (context.HostingEnvironment.IsDevelopment())
        ConfigureOrleansForLocal(siloBuilder);
    else
        ConfigureOrleansForLocal(siloBuilder);
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

void ConfigureOrleansForLocal(ISiloBuilder siloBuilder)
{
    siloBuilder.UseLocalhostClustering(11121, 30001);
    siloBuilder.AddMemoryGrainStorageAsDefault();
    siloBuilder.AddMemoryGrainStorage("auroraStorage");

    siloBuilder.UseDashboard();
    siloBuilder.ConfigureLogging(logging => logging.AddConsole().SetMinimumLevel(LogLevel.Warning));

    siloBuilder.AddStartupTask<BootstrapStartupTask>();
}
using System.Text;
using Aurora.Api.Data;
using Aurora.Api.Extensions;
using Aurora.Api.Routers;
using Aurora.Data.Interfaces;
using Aurora.Grains.Services;
using Aurora.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

ConfigureDatabase(builder);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IDataService<UserRecord, string>, UserDataDataService>();
builder.Services.AddScoped<IOrganizationDataService, OrganizationDataService>();
builder.Services.AddScoped<IUserDataService, UserDataDataService>();

if (builder.Environment.IsDevelopment())
{
    builder.Host.UseOrleans((context, siloBuilder) =>
    {
        siloBuilder.AddOrleansSilo(11111, 30000, false);
    });
}
else
{
    //TODO: Add production configuration
    builder.Host.UseOrleans((context, siloBuilder) =>
    {
        siloBuilder.AddOrleansSilo(11111, 30000, false);
    });
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();

SetupRoutes(app);

void SetupRoutes(WebApplication webApplication)
{
    webApplication.MapGroup("").ServerRoutes();
    webApplication.MapGroup("").OrganizationRoutes();
    webApplication.MapGroup("").UserRoutes();
    webApplication.MapGroup("").AuthRoutes();
}

app.Run();

void ConfigureDatabase(WebApplicationBuilder builder)
{
    var configuration = builder.Configuration;

    var connectionString = configuration.GetConnectionString("Aurora");
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(connectionString));

    builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders();

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

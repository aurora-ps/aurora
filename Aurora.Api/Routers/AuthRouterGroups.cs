using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Aurora.Api.Routers.Models;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace Aurora.Api.Routers;

public static class AuthRouterGroups
{
    private const string UrlFragment = "auth";

    public static RouteGroupBuilder AuthRoutes(this RouteGroupBuilder group)
    {
        group.MapPost($"/{UrlFragment}/login", Login);
        group.MapPost($"/{UrlFragment}/register", Register);
        group.MapPost($"/{UrlFragment}/logout", Logout);
        return group.WithOpenApi();
    }

    private static async Task<IResult> Logout(SignInManager<IdentityUser> signInManager)
    {
        await signInManager.SignOutAsync();
        return TypedResults.Ok();
    }

    private static async Task<IResult> Login(UserManager<IdentityUser> userManager,
        IConfiguration appConfig,
        IValidator<LoginModel> loginValidator,
        LoginModel login)
    {
        var validationResult = await loginValidator.ValidateAsync(login);

        if(!validationResult.IsValid)
            return TypedResults.BadRequest(validationResult.Errors);

        var user = await userManager.FindByNameAsync(login.UserName);
        if (user != null && await userManager.CheckPasswordAsync(user, login.Password))
        {
            var roles = await userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
            {
                new(ClaimTypes.Name, user.UserName),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            authClaims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var token = GetToken(appConfig, authClaims);

            return TypedResults.Ok();
        }

        return TypedResults.Unauthorized();
    }

    private static async Task<IResult> Register(UserManager<IdentityUser> userManager,
        IValidator<RegisterModel> registerValidator,
        RegisterModel register)
    {
        if(!(await registerValidator.ValidateAsync(register)).IsValid)
            return TypedResults.BadRequest();

        var userExists = await userManager.FindByNameAsync(register.UserName);
        if (userExists is not null)
            return TypedResults.Conflict();

        var user = new IdentityUser
        {
            UserName = register.UserName,
            Email = register.Email,
            SecurityStamp = Guid.NewGuid().ToString()
        };
        var result = await userManager.CreateAsync(user, register.Password);
        if (!result.Succeeded)
            return TypedResults.Unauthorized();

        return TypedResults.Ok("User Created");
    }

    private static JwtSecurityToken GetToken(IConfiguration appConfig, List<Claim> authClaims)
    {
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appConfig["JWT:Secret"] ??
                                                                             throw new ApplicationException(
                                                                                 "JWT Secret not properly configured")));

        var token = new JwtSecurityToken(
            appConfig["JWT:ValidIssuer"],
            appConfig["JWT:ValidAudience"],
            expires: DateTime.Now.AddHours(3),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
        );

        return token;
    }
}
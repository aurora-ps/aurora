using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Aurora.Api.Routers;

public static class AuthRouterGroups
{
    public static RouteGroupBuilder AuthRoutes(this RouteGroupBuilder group)
    {
        group.MapPost("/auth/login", Login);
        group.MapPost("/auth/register", Register);
        group.MapPost("/auth/logout", Logout);
        return group.WithOpenApi();
    }

    private static async Task<IResult> Logout([FromServices] SignInManager<IdentityUser> signInManager)
    {
        await signInManager.SignOutAsync();
        return TypedResults.Ok();
    }

    private static async Task<IResult> Login([FromServices] UserManager<IdentityUser> userManager,
        [FromServices] IConfiguration appConfig,
        [FromBody] LoginModel login)
    {
        if (string.IsNullOrEmpty(login?.UserName) || string.IsNullOrEmpty(login?.Password))
            return TypedResults.BadRequest();

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

    private static async Task<IResult> Register([FromServices] UserManager<IdentityUser> userManager,
        [FromBody] RegisterModel register)
    {
        if (string.IsNullOrEmpty(register?.UserName)) return TypedResults.BadRequest();

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

    private class RegisterModel
    {
        [Required(ErrorMessage = "User Name is required")]
        public string? UserName { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }
    }

    private class LoginModel
    {
        [Required(ErrorMessage = "User Name is required")]
        [FromBody]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [FromBody]
        public string? Password { get; set; }
    }
}
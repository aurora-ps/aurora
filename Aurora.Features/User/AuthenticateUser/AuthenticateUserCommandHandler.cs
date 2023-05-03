using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

using Aurora.Interfaces;
using Aurora.Interfaces.Models;
using FluentValidation;
using MediatR;

namespace Aurora.Features.User.AuthenticateUser;

public class AuthenticateUserCommandHandler : IRequestHandler<AuthenticateUserCommand
    , AuthenticateUserCommandResult>
{
    private readonly IConfiguration _appConfig;
    private readonly IValidator<AuthenticateUserCommand> _authenticateUserValidator;
    private readonly IClusterClient _clusterClient;
    private readonly UserManager<AuroraUser> _userManager;

    public AuthenticateUserCommandHandler(UserManager<AuroraUser> userManager,
        IClusterClient clusterClient,
        IConfiguration appConfig,
        IValidator<AuthenticateUserCommand> authenticateUserValidator)
    {
        _userManager = userManager;
        _clusterClient = clusterClient;
        _appConfig = appConfig;
        _authenticateUserValidator = authenticateUserValidator;
    }

    public async Task<AuthenticateUserCommandResult> Handle(AuthenticateUserCommand command,
        CancellationToken cancellationToken)
    {
        var validationResult = await _authenticateUserValidator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            return AuthenticateUserCommandResult.BadRequest(validationResult.Errors);

        var user = await _userManager.FindByNameAsync(command.UserName!);
        if (user == null || !await _userManager.CheckPasswordAsync(user, command.Password!))
            return AuthenticateUserCommandResult.Unauthorized();

        var userRecord = await GetUserDetails(user);
        if (userRecord == null)
        {
            return AuthenticateUserCommandResult.Unauthorized();
        }

        var roles = await _userManager.GetRolesAsync(user);

        var authClaims = new List<Claim>
        {
            new(ClaimTypes.Name, user.UserName!),
            new(ClaimTypes.NameIdentifier, user.Id),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        authClaims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        var token = GetToken(_appConfig, authClaims);

        return AuthenticateUserCommandResult.CreateSuccess(userRecord, user, token);
    }

    private async Task<UserRecord?> GetUserDetails(AuroraUser user)
    {
        var userGrain = _clusterClient.GetGrain<IUserGrain>(user.Id);

        return await userGrain.GetDetailsAsync();
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
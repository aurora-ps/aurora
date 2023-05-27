using System.IdentityModel.Tokens.Jwt;
using Aurora.Interfaces;
using Aurora.Interfaces.Models;
using FluentValidation.Results;

namespace Aurora.Features.User.AuthenticateUser;

public class AuthenticateUserCommandResult
{
    public IList<string> Errors { get; set; }

    public bool Success { get; set; }

    public UserRecord User { get; set; }

    public string Token { get; set; }

    public AuroraUser IdentityUser { get; set; }

    public static AuthenticateUserCommandResult BadRequest(IList<ValidationFailure> validationResultErrors)
    {
        return new AuthenticateUserCommandResult
        {
            Success = false,
            Errors = validationResultErrors.Select(x => x.ErrorMessage).ToList()
        };
    }

    public static AuthenticateUserCommandResult CreateSuccess(UserRecord user, AuroraUser auroraUser,
        JwtSecurityToken token)
    {
        return new AuthenticateUserCommandResult
        {
            Success = true,
            User = user,
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            IdentityUser = auroraUser
        };
    }

    public static AuthenticateUserCommandResult Unauthorized()
    {
        return new AuthenticateUserCommandResult { Success = false };
    }
}
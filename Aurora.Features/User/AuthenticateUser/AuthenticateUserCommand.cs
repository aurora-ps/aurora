using System.ComponentModel.DataAnnotations;
using MediatR;

namespace Aurora.Features.User.AuthenticateUser;

public class AuthenticateUserCommand : IRequest<AuthenticateUserCommandResult>
{
    [Required(ErrorMessage = "User Name is required")]
    public string? UserName { get; set; }

    [Required(ErrorMessage = "Password is required")]
    public string? Password { get; set; }
}
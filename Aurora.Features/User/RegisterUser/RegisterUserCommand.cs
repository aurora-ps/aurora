using System.ComponentModel.DataAnnotations;
using MediatR;

namespace Aurora.Features.User.RegisterUser;

public class RegisterUserCommand : IRequest<RegisterUserResponse>
{
    [Required(ErrorMessage = "User Name is required")]
    public string? UserName { get; set; }

    public string? Email => UserName;

    [Required(ErrorMessage = "Password is required")]
    public string? Password { get; set; }
}
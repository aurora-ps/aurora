using System.ComponentModel.DataAnnotations;
using MediatR;

namespace Aurora.Features.User.AddUser;

public class AddUserCommand : IRequest<AddUserResponse>
{
    [Required] public string UserName { get; set; }

    [Required] public string Email { get; set; }

    [Required] public string Password { get; set; }

    [Required] public string FirstName { get; set; }

    [Required] public string LastName { get; set; }
}
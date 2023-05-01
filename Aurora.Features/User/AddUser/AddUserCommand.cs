using System.ComponentModel.DataAnnotations;
using MediatR;

namespace Aurora.Features.User.AddUser;

public class AddUserCommand : IRequest<AddUserResponse>
{
    [Required] public string UserName { get; set; }

    [Required] public string Email { get; set; }
}
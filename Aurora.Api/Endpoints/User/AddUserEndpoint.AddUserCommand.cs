using System.ComponentModel.DataAnnotations;
using MediatR;

namespace Aurora.Api.Endpoints.User;

public class AddUserCommand : IRequest<AddUserResponse>
{
    [Required]
    public string UserName { get; set; }

    [Required]
    public string Email { get; set; }
}
using System.ComponentModel.DataAnnotations;

namespace Aurora.Api.Routers.Models;

[Serializable]
public class AddUserModel
{
    [Required]
    public string UserName { get; set; }

    [Required]
    public string Email { get; set; }
}
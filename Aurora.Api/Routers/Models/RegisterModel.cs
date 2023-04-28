using System.ComponentModel.DataAnnotations;

namespace Aurora.Api.Routers.Models;

public class RegisterModel
{
    [Required(ErrorMessage = "User Name is required")]
    public string? UserName { get; set; }

    public string? Email => UserName;

    [Required(ErrorMessage = "Password is required")]
    public string? Password { get; set; }
}
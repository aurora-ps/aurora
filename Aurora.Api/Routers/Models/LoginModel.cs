using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Aurora.Api.Routers.Models;

public class LoginModel
{
    [Required(ErrorMessage = "User Name is required")]
    [FromBody]
    public string? UserName { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [FromBody]
    public string? Password { get; set; }
}
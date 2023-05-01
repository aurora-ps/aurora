using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Aurora.Interfaces.Models;

public class AuroraUser : IdentityUser
{
    [StringLength(100)] public string? FirstName { get; set; }

    [StringLength(100)] public string? LastName { get; set; }
}
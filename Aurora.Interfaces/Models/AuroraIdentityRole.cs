using Microsoft.AspNetCore.Identity;

namespace Aurora.Interfaces.Models;

public class AuroraIdentityRole : IdentityRole
{
    public AuroraIdentityRole()
    {
    }

    public AuroraIdentityRole(string roleName) : base(roleName)
    {
    }
}
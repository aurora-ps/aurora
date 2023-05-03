using Aurora.Interfaces.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Aurora.Frontend.Data
{
    public class DataSeeding
    {
        private readonly RoleManager<AuroraIdentityRole> _roleManager;
        private readonly UserManager<AuroraUser> _userManager;

        public DataSeeding(RoleManager<AuroraIdentityRole> roleManager, UserManager<AuroraUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task SeedData()
        {
            bool rolesSeeded = false;

            var roles = await _roleManager.Roles.ToListAsync();
            if (!roles.Any())
            {
                roles = await GetOrSeedRoles(_roleManager);
                rolesSeeded = true;
            }

            await SeedUsers(_userManager, roles, rolesSeeded);
        }

        private static async Task SeedUsers(UserManager<AuroraUser> userManager, List<AuroraIdentityRole> roles, bool rolesSeeded)
        {
            var adminUsers = await userManager.GetUsersInRoleAsync("Administrator");
            if (!adminUsers.Any())
            {
                // Give admin to ALL users for now - need to also check on add-user for first users and add as administrator
                var users = await userManager.Users.ToListAsync();
                foreach (var user in users)
                {
                    await userManager.AddToRoleAsync(user, "Administrator");
                }
            }
        }

        private static async Task<List<AuroraIdentityRole>> GetOrSeedRoles(RoleManager<AuroraIdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new AuroraIdentityRole("Administrator"));
            await roleManager.CreateAsync(new AuroraIdentityRole("User"));
            await roleManager.CreateAsync(new AuroraIdentityRole("Reviewer"));

            return await roleManager.Roles.ToListAsync();
        }
    }
}

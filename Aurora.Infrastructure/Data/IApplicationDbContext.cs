using Aurora.Interfaces.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Aurora.Infrastructure.Data;

public interface IApplicationDbContext : IUnitOfWork
{
    DbSet<AuroraUser> Users { get; set; }
    DbSet<IdentityUserClaim<string>> UserClaims { get; set; }
    DbSet<IdentityUserLogin<string>> UserLogins { get; set; }
    DbSet<IdentityUserToken<string>> UserTokens { get; set; }
    DbSet<IdentityUserRole<string>> UserRoles { get; set; }
    DbSet<AuroraIdentityRole> Roles { get; set; }
    DbSet<IdentityRoleClaim<string>> RoleClaims { get; set; }
}
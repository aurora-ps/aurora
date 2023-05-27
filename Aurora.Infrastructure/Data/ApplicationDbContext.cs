using Aurora.Interfaces.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Aurora.Infrastructure.Data;

public class ApplicationDbContext : IdentityDbContext<AuroraUser, AuroraIdentityRole, string>, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<AuroraUser>().Ignore(p => p.Reports);
        base.OnModelCreating(builder);
    }
}
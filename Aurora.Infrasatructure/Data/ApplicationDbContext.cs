using Aurora.Interfaces.Models;
using Aurora.Interfaces.Models.Reporting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Aurora.Infrastructure.Data;

public class ApplicationDbContext : IdentityDbContext<AuroraUser, AuroraIdentityRole, string>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
}

public interface IReportDbContext
{
    DbSet<Report> Reports { get; set; }
}

public class ReportDbContext : DbContext, IReportDbContext
{
    public ReportDbContext(DbContextOptions<ReportDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Report> Reports { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Report>()
            .HasOne(r => r.Agency)
            .WithMany()
            .HasForeignKey(r => r.AgencyId);

        modelBuilder.Entity<Report>()
            .HasOne(r => r.IncidentType)
            .WithMany()
            .HasForeignKey(r => r.IncidentTypeId);

        modelBuilder.Entity<Report>()
            .HasOne(r => r.User)
            .WithMany()
            .HasForeignKey(r => r.AuroraUserId);

        modelBuilder.Entity<Report>()
            .OwnsOne(r => r.Location);

        modelBuilder.Entity<ReportPerson>()
            .OwnsOne(p => p.PhoneNumber);

        modelBuilder.Entity<AuroraUser>().ToTable("AspNetUsers", t => t.ExcludeFromMigrations());

        base.OnModelCreating(modelBuilder);
    }
}
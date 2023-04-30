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

    DbSet<Agency> Agencies { get; set; }

    DbSet<IncidentType> IncidentTypes { get; set; }
}

public class ReportDbContext : DbContext, IReportDbContext
{
    public ReportDbContext(DbContextOptions<ReportDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Report> Reports { get; set; }

    public virtual DbSet<Agency> Agencies { get; set; }

    public virtual DbSet<IncidentType> IncidentTypes { get; set; }

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

        modelBuilder.Entity<AgencyIncidentType>()
            .HasKey(ai => new { ai.AgencyId, ai.IncidentTypeId });
            
        modelBuilder.Entity<AgencyIncidentType>()
            .HasOne(ai => ai.Agency)
            .WithMany(a => a.IncidentTypes)
            .HasForeignKey(ai => ai.AgencyId);

        modelBuilder.Entity<AgencyIncidentType>()
            .HasOne(ai => ai.IncidentType)
            .WithMany(i => i.AgencyIncidentTypes)
            .HasForeignKey(ai => ai.IncidentTypeId);

        modelBuilder.Entity<IncidentType>().HasData(GetIncidentTypes());

        modelBuilder.Entity<Agency>().HasData(GetAgencies());

        modelBuilder.Entity<AgencyIncidentType>()
            .HasData(new List<AgencyIncidentType>()
            {
                new() { AgencyId = "87D773C9-9420-4B42-857D-3DB4783476BC", IncidentTypeId = "EB4F4F16-7B39-448D-9215-B578335F08DE" },
                new() { AgencyId = "87D773C9-9420-4B42-857D-3DB4783476BC", IncidentTypeId = "AB0F1C6E-E6E5-489D-A88E-8010AB8A358A" },
                new() { AgencyId = "87D773C9-9420-4B42-857D-3DB4783476BC", IncidentTypeId = "5D035B97-5CB0-4FA9-978E-7B34A250426E" },
                new() { AgencyId = "87D773C9-9420-4B42-857D-3DB4783476BC", IncidentTypeId = "105B5539-879E-4F8C-B6F1-2C493CF81FAB" },
                new() { AgencyId = "87D773C9-9420-4B42-857D-3DB4783476BC", IncidentTypeId = "A7A975E0-5952-434E-9D87-F8B049D84016" },
                new() { AgencyId = "BAD172CE-D1D5-4AD0-809C-9BD04D6D22AC", IncidentTypeId = "EB4F4F16-7B39-448D-9215-B578335F08DE" },
                new() { AgencyId = "BAD172CE-D1D5-4AD0-809C-9BD04D6D22AC", IncidentTypeId = "AB0F1C6E-E6E5-489D-A88E-8010AB8A358A" },
                new() { AgencyId = "BAD172CE-D1D5-4AD0-809C-9BD04D6D22AC", IncidentTypeId = "5D035B97-5CB0-4FA9-978E-7B34A250426E" },
                new() { AgencyId = "BAD172CE-D1D5-4AD0-809C-9BD04D6D22AC", IncidentTypeId = "105B5539-879E-4F8C-B6F1-2C493CF81FAB" },
                new() { AgencyId = "BAD172CE-D1D5-4AD0-809C-9BD04D6D22AC", IncidentTypeId = "A7A975E0-5952-434E-9D87-F8B049D84016" },
            });

        base.OnModelCreating(modelBuilder);
    }

    private static List<Agency> GetAgencies()
    {
        return new List<Agency>()
        {
            new("87D773C9-9420-4B42-857D-3DB4783476BC", "Durham - CRT"),
            new("BAD172CE-D1D5-4AD0-809C-9BD04D6D22AC", "Person - CRT"),
        };
    }

    private static List<IncidentType> GetIncidentTypes()
    {
        return new List<IncidentType>()
        {
            new()
            {
                Id = "EB4F4F16-7B39-448D-9215-B578335F08DE", Name = "Death Call", CollectTime = true, RequiresTime = true,
                CollectLocation = true,
                CollectPerson = true
            },
            new() { Id = "AB0F1C6E-E6E5-489D-A88E-8010AB8A358A", Name = "Administration" },
            new()
            {
                Id = "5D035B97-5CB0-4FA9-978E-7B34A250426E", Name = "Training", CollectTime = true, RequiresTime = true, CollectLocation = true
            },
            new() { Id = "105B5539-879E-4F8C-B6F1-2C493CF81FAB", Name = "Other", CollectTime = true },
            new()
            {
                Id = "A7A975E0-5952-434E-9D87-F8B049D84016", Name = "Crisis Call", CollectTime = true, RequiresTime = true,
                CollectLocation = true
            },
        };
    }
}
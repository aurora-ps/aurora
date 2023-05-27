using Aurora.Interfaces.Models.Reporting;
using Microsoft.EntityFrameworkCore;

namespace Aurora.Infrastructure.Data;

public interface IReportDbContext : IUnitOfWork
{
    DbSet<Report> Reports { get; set; }

    DbSet<Agency> Agencies { get; set; }

    DbSet<IncidentType> IncidentTypes { get; set; }
}
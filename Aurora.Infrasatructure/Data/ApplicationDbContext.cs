﻿using Aurora.Interfaces.Models;
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
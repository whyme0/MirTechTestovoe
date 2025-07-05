﻿using Microsoft.EntityFrameworkCore;
using MirTechTestovoe.Server.Models;

namespace MirTechTestovoe.Server.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; } = null!;
    }
}
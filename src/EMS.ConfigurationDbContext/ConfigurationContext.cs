using System;
using EMS.Models;
using EMS.Models.Configuration;
using Microsoft.EntityFrameworkCore;

namespace EMS.ConfigurationDbContext
{
    public class ConfigurationContext : DbContext
    {
        public ConfigurationContext(DbContextOptions<ConfigurationContext> options) : base(options)
        {
            
        }

        public DbSet<WorkerConfiguration> Workers { get; set; }
    }
}
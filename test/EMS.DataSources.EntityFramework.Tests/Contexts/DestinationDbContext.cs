using EMS.DataSources.EntityFramework.Tests.Models;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace EMS.DataSources.EntityFramework.Tests.Contexts
{
    public class DestinationDbContext : DbContext
    {
        public DestinationDbContext(SqliteConnection connection) : this(
            new DbContextOptionsBuilder<DestinationDbContext>()
                .UseSqlite(connection).Options)
        {
        }

        public DestinationDbContext(DbContextOptions<DestinationDbContext> options) : base(options)
        {
        }

        public DbSet<DestinationUser> Users { get; set; }
    }
}
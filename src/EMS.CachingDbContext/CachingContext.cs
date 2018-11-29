using EMS.CachingDbContext.Models;
using Microsoft.EntityFrameworkCore;

namespace EMS.CachingDbContext
{
    public class CachingContext : DbContext
    {
        public CachingContext(DbContextOptions<CachingContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
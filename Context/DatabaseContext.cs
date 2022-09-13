using Microsoft.EntityFrameworkCore;
using SteamQueue.Entities;

namespace SteamQueue.Context
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Position> Positions { get; set; }
        public DbSet<Line> Lines { get; set; }
        public DbSet<SteamAccount> Accounts { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

        }
    }
}

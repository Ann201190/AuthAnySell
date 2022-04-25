using Auth.Business.Entities;
using Microsoft.EntityFrameworkCore;

namespace Auth.Business.Context
{

    public class CustomDbContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
       
        public CustomDbContext(DbContextOptions<CustomDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public static DbContextOptions GetOptions(string connectionString)
        {
            return SqlServerDbContextOptionsExtensions.UseSqlServer(new DbContextOptionsBuilder(), connectionString).Options;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Seed();
        }
    }
}

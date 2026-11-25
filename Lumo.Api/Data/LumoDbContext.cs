using Lumo.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Lumo.Api.Data
{
    public class LumoDbContext : DbContext
    {
        public LumoDbContext(DbContextOptions<LumoDbContext> options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().Property(u => u.Role).HasDefaultValue("User");

        }
    }
}

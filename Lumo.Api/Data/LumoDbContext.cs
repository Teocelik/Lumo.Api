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
    }
}

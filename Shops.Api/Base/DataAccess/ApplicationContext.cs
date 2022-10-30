using Microsoft.EntityFrameworkCore;
using Shops.Api.Entities;

namespace Shops.Api.Base
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Shop> Shops { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
using EFCoreConsoleApp.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace EFCoreConsoleApp.Data.Context
{
    public class EcommerceContext : DbContext
    {
        public EcommerceContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Database=shopping;Username=postgres;Password=Sancho38293!");
        }

        public DbSet<Brand> Brands { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductOrder> ProductOrders { get; set; }
        public DbSet<Review> Reviews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}

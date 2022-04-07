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
            optionsBuilder.UseNpgsql("Server=127.0.0.1;Port=5432;Database=EFCoreConsole;User Id=postgres;Password=Sancho38293!;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>()
                .HasOne(a => a.User)
                .WithMany(u => u.Addresses)
                .HasForeignKey(a => a.UserId);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId);

            modelBuilder.Entity<Review>()
                .HasOne(r => r.Product)
                .WithMany(p => p.Reviews)
                .HasForeignKey(r => r.ProductId);

            modelBuilder.Entity<Review>()
                .HasOne(r => r.User)
                .WithMany(u => u.Reviews)
                .HasForeignKey(r => r.UserId);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Brand)
                .WithMany(b => b.Products)
                .HasForeignKey(p => p.BrandId);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.ProductCategory)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.ProductCategoryId);

            modelBuilder.Entity<Review>()
                .HasOne(r => r.Product)
                .WithMany(p => p.Reviews)
                .HasForeignKey(r => r.ProductId);

            modelBuilder.Entity<ProductOrder>()
                .HasKey(o => new { o.ProductId, o.OrderId });

            modelBuilder.Entity<ProductOrder>()
                .HasOne(p => p.Product)
                .WithMany(p => p.ProductOrders)
                .HasForeignKey(p => p.ProductId);

            modelBuilder.Entity<ProductOrder>()
                .HasOne(o => o.Order)
                .WithMany(o => o.ProductOrders)
                .HasForeignKey(o => o.OrderId);

            modelBuilder.Entity<ProductCategory>()
                .HasOne(c => c.ParentCategory);

            modelBuilder.Entity<ProductCategorySection>()
                .HasKey(s => new { s.SectionId, s.ProductCategoryId });

            modelBuilder.Entity<ProductCategorySection>()
                .HasOne(p => p.ProductCategory)
                .WithMany(s => s.ProductCategorySections)
                .HasForeignKey(p => p.ProductCategoryId);

            modelBuilder.Entity<ProductCategorySection>()
                .HasOne(s => s.Section)
                .WithMany(p => p.ProductCategorySections)
                .HasForeignKey(s => s.SectionId);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithOne(r => r.User)
                .HasForeignKey<User>(r => r.RoleId);
        }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ProductCategorySection> ProductCategorySections { get; set; }
        public DbSet<ProductOrder> ProductOrders { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
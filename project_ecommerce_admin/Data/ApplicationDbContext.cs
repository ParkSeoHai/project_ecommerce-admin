using Microsoft.EntityFrameworkCore;
using project_ecommerce_admin.Models;

namespace project_ecommerce_admin.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        // Create table SQL Server
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<AddressShop> AddressShops { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<CustomerAddress> CustomerAddresses { get; set; }
        public DbSet<FlashSale> FlashSales { get; set; }
        public DbSet<FlashSaleItem> FlashSaleItems { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<ProductShop> ProductShops { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<Status> Statuses { get; set; }

        // Create Unique
        protected override void OnModelCreating(ModelBuilder builders)
        {
            // Unique table Category
            builders.Entity<Category>(entity =>
            {
                entity.HasIndex(e => e.Name).IsUnique();
            });
            // Unique table Product
            builders.Entity<Product>(entity =>
            {
                entity.HasIndex(e => e.Name).IsUnique();
            });
            // Unique table News
            builders.Entity<News>(entity =>
            {
                entity.HasIndex(e => e.Title).IsUnique();
            });
            // Unique table Customer
            builders.Entity<Customer>(entity =>
            {
                entity.HasIndex(e => e.Email).IsUnique();
                entity.HasIndex(e => e.EmailConfirmed).IsUnique();
            });
        }
    }
}

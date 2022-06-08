using EF_FluentAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EF_FluentAPI.DbContexts
{
    public class DBContext : DbContext
    {
        public DBContext() => Database.EnsureCreated();

        public DBContext(DbContextOptions<DBContext> options) : base(options) { }

        public DbSet<Customer> Customers { get; set; } = null!;
        public DbSet<Credential> Credentials { get; set; } = null!;
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<Cart> Carts { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Customer>()
                .ToTable("Customer")
                .HasOne(u => u.Credential)
                .WithOne(u => u.Customer)
                .HasForeignKey<Credential>(u => u.CustomerId);

            builder.Entity<Order>()
                .ToTable("Order")
                .HasOne(d => d.Customer)
                .WithMany(p => p.Orders)
                .HasForeignKey(d => d.CustomerId);

            builder.Entity<Product>()
                .ToTable("Product")
                .HasMany(d => d.Orders)
                .WithMany(p => p.Products)
                .UsingEntity<Dictionary<string, object>>(
                    "ProductOrder",
                    u => u.HasOne<Order>().WithMany().HasForeignKey("OrderId"),
                    p => p.HasOne<Product>().WithMany().HasForeignKey("ProductId"),
                    j =>
                    {
                        j.HasKey("ProductId", "OrderId");
                        j.ToTable("ProductOrder");
                    });

            builder.Entity<Cart>()
                .ToTable("Cart")
                .HasOne(u => u.Customer)
                .WithOne(u => u.Cart);

            builder.Entity<Product>()
                .HasMany(d => d.Carts)
                .WithMany(p => p.Products)
                .UsingEntity<Dictionary<string, object>>(
                    "ProductCart",
                    u => u.HasOne<Cart>().WithMany().HasForeignKey("CartId"),
                    p => p.HasOne<Product>().WithMany().HasForeignKey("ProductId"),
                    j =>
                    {
                        j.HasKey("ProductId", "CartId");
                        j.ToTable("ProductCart");
                    });
        }            
    }
}

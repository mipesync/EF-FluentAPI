using EF_FluentAPI.Models;
using EF_FluentAPI.Models.Intermediate_Entities;
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
        public DbSet<ProductCart> ProductCarts { get; set; } = null!;
        public DbSet<ProductOrder> ProductOrders { get; set; } = null!;

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
                .HasMany(d => d.Orders)
                .WithMany(p => p.Products)
                .UsingEntity<ProductOrder>(
                j => j
                    .HasOne(u => u.Order)
                    .WithMany(p => p.ProductOrders)
                    .HasForeignKey(o => o.OrderId),
                o => o
                    .HasOne(pt => pt.Product)
                    .WithMany(p => p.ProductOrders)
                    .HasForeignKey(pt => pt.ProductId),
                j =>
                {
                    j.HasKey("Id");
                    j.ToTable("ProductOrder");
                });

            builder.Entity<Cart>()
                .ToTable("Cart")
                .HasOne(u => u.Customer)
                .WithOne(u => u.Cart);

            builder.Entity<Product>()
                .HasMany(d => d.Carts)
                .WithMany(p => p.Products)
                .UsingEntity<ProductCart>(
                j => j
                    .HasOne(u => u.Cart)
                    .WithMany(p => p.ProductCarts)
                    .HasForeignKey(o => o.CartId),
                o => o
                    .HasOne(pt => pt.Product)
                    .WithMany(p => p.ProductCarts)
                    .HasForeignKey(pt => pt.ProductId),
                j =>
                {
                    j.HasKey("Id");
                    j.ToTable("ProductCart");
                });

        }
    }
}

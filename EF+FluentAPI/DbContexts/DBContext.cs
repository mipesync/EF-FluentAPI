using EF_FluentAPI.DbContexts.Configure;
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
            builder.ApplyConfiguration(new ConfigureCustomers());
            builder.ApplyConfiguration(new ConfigureOrders());
            builder.ApplyConfiguration(new ConfigureProducts());
            builder.ApplyConfiguration(new ConfigureCarts());
        }
    }
}

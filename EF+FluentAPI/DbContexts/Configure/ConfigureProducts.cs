using EF_FluentAPI.Models;
using EF_FluentAPI.Models.Intermediate_Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EF_FluentAPI.DbContexts.Configure
{
    public class ConfigureProducts : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasMany(d => d.Orders)
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
                    j.ToTable("ProductOrders");
                });

            builder.HasMany(d => d.Carts)
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
                    j.ToTable("ProductCarts");
                });
        }
    }
}

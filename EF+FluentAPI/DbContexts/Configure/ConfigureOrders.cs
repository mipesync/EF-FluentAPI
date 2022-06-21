using EF_FluentAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EF_FluentAPI.DbContexts.Configure
{
    public class ConfigureOrders : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");
            builder.HasOne(d => d.Customer)
                .WithMany(p => p.Orders)
                .HasForeignKey(d => d.CustomerId);
        }
    }
}

using EF_FluentAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EF_FluentAPI.DbContexts.Configure
{
    public class ConfigureCarts : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.ToTable("Carts")
                .HasOne(u => u.Customer)
                .WithOne(u => u.Cart);
        }
    }
}

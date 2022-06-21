using EF_FluentAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EF_FluentAPI.DbContexts.Configure
{
    public class ConfigureCustomers : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customers");
            builder.HasOne(u => u.Credential)
                .WithOne(u => u.Customer)
                .HasForeignKey<Credential>(u => u.CustomerId);
        }
    }
}

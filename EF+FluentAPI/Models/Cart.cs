using EF_FluentAPI.Models.Intermediate_Entities;
using System.Text.Json.Serialization;

namespace EF_FluentAPI.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public string? CustomerId { get; set; }
        public decimal TotalPrice { get; set; }
        public int Count { get; set; }

        public Customer? Customer { get; set; }
        public ICollection<Product>? Products { get; set; }
        public ICollection<ProductCart>? ProductCarts { get; set; }
    }
}

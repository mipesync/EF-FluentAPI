using EF_FluentAPI.Models.Intermediate_Entities;
using EF_FluentAPI__Front_.Models.Intermediate_Entities;
using System.Text.Json.Serialization;

namespace EF_FluentAPI__Front_.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }

        [JsonIgnore]
        public ICollection<Order>? Orders { get; set; }
        [JsonIgnore]
        public ICollection<Cart>? Carts { get; set; }
        public ICollection<ProductCart>? ProductCarts { get; set; }
        public ICollection<ProductOrder>? ProductOrders { get; set; }
    }
}

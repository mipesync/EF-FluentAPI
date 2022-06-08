using System.Text.Json.Serialization;

namespace EF_FluentAPI.Models
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
    }
}

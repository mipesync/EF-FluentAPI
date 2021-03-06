using System.Text.Json.Serialization;
using EF_FluentAPI.Generators;
using EF_FluentAPI.Models.Intermediate_Entities;

namespace EF_FluentAPI.Models
{
    public class Order
    {
        public Order()
        {
            OrderDate = DateTime.Now;
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }
        public string CustomerId { get; set; } = null!;
        public bool IsCompleted { get; set; }

        [JsonIgnore]
        public Customer Customer { get; set; } = null!;
        public ICollection<Product>? Products { get; set; }
        public ICollection<ProductOrder>? ProductOrders { get; set; }

    }
}

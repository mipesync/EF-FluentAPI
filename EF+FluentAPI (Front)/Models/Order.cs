using System.Text.Json.Serialization;
using EF_FluentAPI__Front_.Generators;

namespace EF_FluentAPI__Front_.Models
{
    public class Order
    {
        public Order()
        {
            Name = new OrderNameGenerator().Generate();
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
        public ICollection<Product> Products { get; set; } = null!;

    }
}

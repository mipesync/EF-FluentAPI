using System.Text.Json.Serialization;

namespace EF_FluentAPI__Front_.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public string? CustomerId { get; set; }

        public Customer? Customer { get; set; }
        public ICollection<Product>? Products { get; set; }
    }
}

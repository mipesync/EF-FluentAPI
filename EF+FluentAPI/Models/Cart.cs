namespace EF_FluentAPI.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public string? CustomerId { get; set; }

        public Customer? Customer { get; set; }
        public ICollection<Product>? Products { get; set; }
    }
}

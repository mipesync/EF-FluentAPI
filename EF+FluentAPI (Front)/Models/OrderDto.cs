namespace EF_FluentAPI__Front_.Models;

public class OrderDto
{
    public decimal TotalPrice { get; set; }
    public ICollection<Product> Products { get; set; } = null!;
}
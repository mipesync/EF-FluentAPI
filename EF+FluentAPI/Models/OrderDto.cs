namespace EF_FluentAPI.Models;

public class OrderDto
{
    public string Name { get; set; } = null!;
    public decimal TotalPrice { get; set; }
    public ICollection<Product> Products { get; set; } = null!;
}
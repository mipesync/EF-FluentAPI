namespace EF_FluentAPI.Models;

public class OrderDto
{
    public string Name { get; set; } = null!;
    public decimal TotalPrice { get; set; }
    public DateTime OrderDate { get; set; }
    public bool IsCompleted { get; set; }
    
    public ICollection<Product> Products { get; set; } = null!;
}
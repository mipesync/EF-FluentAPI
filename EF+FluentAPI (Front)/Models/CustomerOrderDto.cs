namespace EF_FluentAPI__Front_.Models;

public class CustomerOrderDto
{
    public Customer Customer { get; set; } = null!;
    public Cart OrderDto { get; set; } = null!;
}
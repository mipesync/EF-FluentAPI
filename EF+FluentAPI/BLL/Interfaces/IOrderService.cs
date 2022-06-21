using EF_FluentAPI.Models;

namespace EF_FluentAPI.BLL.Interfaces
{
    public interface IOrderService
    {
        List<Order> GetAll();
        List<Order> GetOrdersByCustomerId(string customerId);
        List<Order> GetOrdersByCustomerPhone(string customerPhone);
        List<Order> GetOrdersByDate(DateTime date);
        Task<Order?> GetByName(string name);
        Task<Order?> GetById(int id);
        Task<Order?> PlaceAnOrder(OrderDto orderDto, string customerId, Customer customer, Cart cart);
    }
}

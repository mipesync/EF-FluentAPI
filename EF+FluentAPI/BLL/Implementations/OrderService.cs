using EF_FluentAPI.BLL.Interfaces;
using EF_FluentAPI.DbContexts;
using EF_FluentAPI.Generators;
using EF_FluentAPI.Models;
using EF_FluentAPI.Models.Intermediate_Entities;
using Microsoft.EntityFrameworkCore;

namespace EF_FluentAPI.BLL.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly DBContext _dbContext;

        public OrderService(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Order> GetAll()
        {
            return _dbContext.Orders.Include(u => u.Products).Include(u => u.Customer).ToList();
        }

        public List<Order> GetOrdersByCustomerId(string customerId)
        {
            return _dbContext.Orders.Include(u => u.Products).Include(u => u.Customer).Where(u => u.CustomerId == customerId).ToList();
        }

        public List<Order> GetOrdersByCustomerPhone(string customerPhone)
        {
            return _dbContext.Orders.Include(u => u.Products).Where(u => u.Customer.Phone.Contains(customerPhone.Trim())).ToList();
        }

        public List<Order> GetOrdersByDate(DateTime date)
        {
            return _dbContext.Orders.Include(u => u.Products).Include(u => u.Customer).Where(u => u.OrderDate.Date == date).ToList();
        }

        public async Task<Order?> GetByName(string name)
        {
            return await _dbContext.Orders.Include(u => u.Products).Include(u => u.Customer).FirstOrDefaultAsync(u => u.Name == name);
        }

        public async Task<Order?> GetById(int id)
        {
            return await _dbContext.Orders.Include(u => u.Products).Include(u => u.Customer).FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<Order?> PlaceAnOrder(OrderDto orderDto, string customerId, Customer customer, Cart cart)
        {
            var order = new Order
            {
                Name = new OrderNameGenerator().Generate(_dbContext),
                Customer = customer!,
                IsCompleted = true,
                TotalPrice = orderDto.TotalPrice
            };

            foreach (var product in orderDto.Products)
            {
                _dbContext.Products.Attach(product);
            }
            await _dbContext.Orders.AddAsync(order);
            foreach (var product in orderDto.Products)
            {
                for (int i = 0; i < product.ProductCarts!.Count; i++)
                {
                    await _dbContext.ProductOrders.AddAsync(new ProductOrder { Order = order, Product = product });
                }
            }

            cart!.ProductCarts!.Clear();
            cart.Products!.Clear();
            cart.TotalPrice = 0;
            cart.Count = 0;
            _dbContext.Carts.Update(cart);

            await _dbContext.SaveChangesAsync();

            return order;
        }
    }
}

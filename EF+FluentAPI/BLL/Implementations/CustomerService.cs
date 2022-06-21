using EF_FluentAPI.BLL.Interfaces;
using EF_FluentAPI.DbContexts;
using EF_FluentAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EF_FluentAPI.BLL.Implementations
{
    public class CustomerService : ICustomerService
    {
        private readonly DBContext _dbContext;

        public CustomerService(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Customer> GetAll()
        {
            return _dbContext.Customers.Include(u => u.Orders).ToList();
        }

        public async Task<Customer?> GetById(string id)
        {
            return await _dbContext.Customers.Include(u => u.Orders).FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<Customer?> Create(Customer customer, Credential credential)
        {
            customer.Credential = credential;

            await _dbContext.Customers.AddAsync(customer);
            await _dbContext.SaveChangesAsync();

            return customer;
        }

        public async Task<Customer?> Edit(Customer customerData, Customer customer)
        {
            customer = customerData;
            customer.Id = customerData.Id;

            _dbContext.Update(customer);
            await _dbContext.SaveChangesAsync();

            return customer;
        }
    }
}

using EF_FluentAPI.Models;

namespace EF_FluentAPI.BLL.Interfaces
{
    public interface ICustomerService
    {
        List<Customer> GetAll();
        Task<Customer?> GetById(string id);
        Task<Customer?> Create(Customer customer, Credential credential);
        Task<Customer?> Edit(Customer customerData, Customer customer);
    }
}

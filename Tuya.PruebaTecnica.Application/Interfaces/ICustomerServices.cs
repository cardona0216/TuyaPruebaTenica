using Tuya.PruebaTecnica.Domain.Models;

namespace Tuya.PruebaTecnica.Application.Interfaces
{
    public interface ICustomerServices
    {
        Task<IEnumerable<Customer>> GetAllCustomersAsync();
        Task<Customer> GetCustomerByIdAsync(int customerId);
        Task<Customer> CreateCustomerAsync(Customer customer);
        Task<Customer> UpdateCustomerAsync(Customer customer);
        Task<Customer> DeleteCustomerAsync(int customerId);
    }
}

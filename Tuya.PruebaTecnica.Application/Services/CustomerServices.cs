using Tuya.PruebaTecnica.Application.Interfaces;
using Tuya.PruebaTecnica.Domain.Models;

namespace Tuya.PruebaTecnica.Application.Features
{
    public class CustomerServices : ICustomerServices
    {
        private readonly IGenericRepository<Customer> _customerRepository;

        public CustomerServices(IGenericRepository<Customer> customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            return await _customerRepository.GetAllAsync();
        }

        public async Task<Customer> GetCustomerByIdAsync(int customerId)
        {
            return await _customerRepository.GetByIdAsync(customerId);
        }

        public async Task<Customer> CreateCustomerAsync(Customer customer)
        {
            return await _customerRepository.AddAsync(customer);
        }

        public async Task<Customer> UpdateCustomerAsync(Customer customer)
        {
            return await _customerRepository.UpdateAsync(customer);
        }

        public async Task<Customer> DeleteCustomerAsync(int productId)
        {
            return await _customerRepository.DeleteAsync(productId);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tuya.PruebaTecnica.Domain.Models;

namespace Tuya.PruebaTecnica.Application.Interfaces
{
    public interface IProductServices
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product> GetProductByIdAsync(int customerId);
        Task<Product> CreateProductAsync(Product customer);
        Task<Product> UpdateProductAsync(Product customer);
        Task<Product> DeleteProductAsync(int customerId);
    }
}

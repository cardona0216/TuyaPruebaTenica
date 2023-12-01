using Tuya.PruebaTecnica.Application.Interfaces;
using Tuya.PruebaTecnica.Domain.Models;

namespace Tuya.PruebaTecnica.Application.Features
{
    public class ProductServices : IProductServices
    {
        private readonly IGenericRepository<Product> _productRepository;

        public ProductServices(IGenericRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _productRepository.GetAllAsync();
        }

        public async Task<Product> GetProductByIdAsync(int productId)
        {
            return await _productRepository.GetByIdAsync(productId);
        }

        public async Task<Product> CreateProductAsync(Product product)
        {
            return await _productRepository.AddAsync(product);
        }

        public async Task<Product> UpdateProductAsync(Product product)
        {
            return await _productRepository.UpdateAsync(product);
        }

        public async Task<Product> DeleteProductAsync(int productId)
        {
            return await _productRepository.DeleteAsync(productId);
        }
    }
}

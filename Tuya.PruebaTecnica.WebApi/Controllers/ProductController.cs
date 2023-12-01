using Microsoft.AspNetCore.Mvc;
using Tuya.PruebaTecnica.Application.Interfaces;
using Tuya.PruebaTecnica.Domain.Models;

namespace Tuya.PruebaTecnica.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductServices _productService;

        public ProductController(IProductServices productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Product product)
        {
            if (product == null)
            {
                return BadRequest("Product is null.");
            }

            var createdProduct = await _productService.CreateProductAsync(product);
            return CreatedAtAction(nameof(Get), new { id = createdProduct.Id }, createdProduct);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Product product)
        {
            if (product == null || product.Id != id)
            {
                return BadRequest("Product is null or id mismatch.");
            }

            await _productService.UpdateProductAsync(product);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _productService.DeleteProductAsync(id);
            return NoContent();
        }
    }
}

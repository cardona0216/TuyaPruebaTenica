using Microsoft.AspNetCore.Mvc;
using Tuya.PruebaTecnica.Application.Interfaces;
using Tuya.PruebaTecnica.Domain.Models;

namespace Tuya.PruebaTecnica.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerServices _customerService;

        public CustomerController(ICustomerServices customerService)
        {
            _customerService = customerService;
        }

        // GET: customer
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var customers = await _customerService.GetAllCustomersAsync();
            return Ok(customers);
        }

        // GET customer/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var customer = await _customerService.GetCustomerByIdAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        // POST customer
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Customer customer)
        {
            if (customer == null)
            {
                return BadRequest("Customer is null.");
            }

            var createdCustomer = await _customerService.CreateCustomerAsync(customer);
            return CreatedAtAction(nameof(Get), new { id = createdCustomer.Id }, createdCustomer);
        }

        // PUT customer/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Customer customer)
        {
            if (customer == null || customer.Id != id)
            {
                return BadRequest("Customer is null or id mismatch.");
            }

            await _customerService.UpdateCustomerAsync(customer);
            return NoContent();
        }

        // DELETE customer/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _customerService.DeleteCustomerAsync(id);
            return NoContent();
        }
    }
}

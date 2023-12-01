using Microsoft.AspNetCore.Mvc;
using Tuya.PruebaTecnica.Application.Interfaces;
using Tuya.PruebaTecnica.Domain.Models;
using Tuya.PruebaTecnica.WebApi.Dtos;

namespace Tuya.PruebaTecnica.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            var orderDto = MapToDto(order);
            return Ok(orderDto);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] OrderDTO orderDto)
        {
            if (orderDto == null)
            {
                return BadRequest("Order data is null.");
            }

            var order = MapToEntity(orderDto);
            var createdOrder = await _orderService.CreateOrderAsync(order);
            
            var createdOrderDto = MapToDto(createdOrder);
            return CreatedAtAction(nameof(Get), new { id = createdOrderDto.OrderId }, createdOrderDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Order order)
        {
            if (order == null || order.Id != id)
            {
                return BadRequest("Order is null or id mismatch.");
            }

            await _orderService.UpdateOrderAsync(order);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _orderService.DeleteOrderAsync(id);
            return NoContent();
        }

        private OrderDTO MapToDto(Order order)
        {
            var orderDto = new OrderDTO
            {
                OrderId = order.Id,
                CustomerId = order.CustomerId,
                OrderDate = order.OrderDate,
                Status = order.Status,
                TotalAmount = order.TotalAmount,
                OrderItems = order.OrderItems.Select(oi => new OrderItemDTO
                {
                    OrderItemId = oi.OrderId,
                    ProductId = oi.ProductId,
                    Quantity = oi.Quantity,
                    Price = oi.Price
                }).ToList()
            };

            return orderDto;
        }

        private Order MapToEntity(OrderDTO orderDto)
        {
            var order = new Order
            {
                Id = orderDto.OrderId,
                CustomerId = orderDto.CustomerId,
                OrderDate = orderDto.OrderDate,
                Status = orderDto.Status,
                TotalAmount = orderDto.TotalAmount,
                OrderItems = orderDto.OrderItems.Select(oi => new OrderItem
                {
                    OrderId = oi.OrderItemId,
                    ProductId = oi.ProductId,
                    Quantity = oi.Quantity,
                    Price = oi.Price
                }).ToList()
            };

            return order;
        }
    }
}

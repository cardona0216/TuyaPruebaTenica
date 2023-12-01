using Tuya.PruebaTecnica.Application.Interfaces;
using Tuya.PruebaTecnica.Domain.Models;

namespace Tuya.PruebaTecnica.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IGenericRepository<Order> _orderRepository;
        private readonly IGenericRepository<OrderItem> _orderItemRepository;

        public OrderService(IGenericRepository<Order> orderRepository, IGenericRepository<OrderItem> orderItemRepository)
        {
            _orderRepository = orderRepository;
            _orderItemRepository = orderItemRepository;
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {            
            return await _orderRepository.GetAllAsync();
        }

        public async Task<Order> GetOrderByIdAsync(int orderId)
        {
            
            return await _orderRepository.GetByIdAsync(orderId);
        }

        public async Task<Order> CreateOrderAsync(Order order)
        {
            if (order.OrderItems == null)
            {
                throw new InvalidOperationException("Cannot create an order without order items.");
            }

            await _orderRepository.AddAsync(order);

            foreach (var item in order.OrderItems)
            {
                item.OrderId = order.Id;
                await _orderItemRepository.AddAsync(item);
            }

            return order;
        }

        public async Task UpdateOrderAsync(Order order)
        {
            if (order.OrderItems == null)
            {
                throw new InvalidOperationException("Order items are required for updating an order.");
            }
            
            await _orderRepository.UpdateAsync(order);

            foreach (var item in order.OrderItems)
            {
                if (item.OrderId == 0)
                {
                    await _orderItemRepository.AddAsync(item);
                }
                else
                {
                    await _orderItemRepository.UpdateAsync(item);
                }
            }           
        }

        public async Task DeleteOrderAsync(int orderId)
        {
            var order = await _orderRepository.GetByIdAsync(orderId);
            if (order == null)
            {
                throw new InvalidOperationException("Order not found.");
            }

            foreach (var item in order.OrderItems)
            {
                await _orderItemRepository.DeleteAsync(item.OrderId);
            }

            await _orderRepository.DeleteAsync(orderId);
        }
    }
}

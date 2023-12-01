using Tuya.PruebaTecnica.Domain.Models;

namespace Tuya.PruebaTecnica.Application.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task<Order> GetOrderByIdAsync(int orderId);
        Task<Order> CreateOrderAsync(Order order);
        Task UpdateOrderAsync(Order order);
        Task DeleteOrderAsync(int orderId);        
    }
}

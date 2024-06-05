

using Application.Common.Exceptions;
using Domain.Entities;
using Domain.Enums;

namespace Application.Services
{
    public interface IOrderService
    {
        Task<Order> GetOrderAsync(int id);
        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task<Order> AddOrderAsync(Order order);
        Task UpdateOrderAsync(Order order);
        Task<Result> RemoveLoginOrderAsync(int id);
        Task<Result<Order>> SetOrderStatus(int orderId, OrderStatusTypeEnum status);
    }
}

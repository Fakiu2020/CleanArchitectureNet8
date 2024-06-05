using Domain.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Application.Common.Exceptions;
namespace Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IAssetService _assetService;

        public OrderService(IOrderRepository orderRepository, IAssetService assetService)
        {
            _orderRepository = orderRepository;
            _assetService = assetService;
        }

        public async Task<Order> GetOrderAsync(int id)
        {
            var order = await _orderRepository.GetByIdAsync(id, true);
            if (order is null)
                throw new ArgumentException("Order not found", nameof(id));

            return order;
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _orderRepository.GetAllAsync(true);
        }

        public async Task<Order> AddOrderAsync(Order order)
        {
            var existAsset = await _assetService.GetAssetByNameAsync(order.AssetName) ?? throw new ArgumentException("Asset not found", "AssetName");
            if (order.Quantity <= 0)
                throw new ArgumentException("Quantity must be greater than zero", nameof(order.Quantity));

            if (order.Price <= 0 && existAsset.Type != Domain.Enums.AssetTypeEnum.Stock)
                throw new ArgumentException("Price must be greater than zero", nameof(order.Price));

            order.CalculateTotalAmount(existAsset.Type, existAsset.UnitPrice);
            await _orderRepository.Add(order);
            await _orderRepository.SaveChangesAsync();
            return order;
        }

        public async Task UpdateOrderAsync(Order order)
        {
            await _orderRepository.Update(order);
            await _orderRepository.SaveChangesAsync();
        }

        public async Task<Result<Order>> SetOrderStatus(int orderId, OrderStatusTypeEnum status)
        {
            try
            {
                var order = await _orderRepository.GetByIdAsync(orderId, true);
                if (order is null)
                    return Result<Order>.Failure(new[] { "Order not found." });
                order.UpdateStatus(status);
                await UpdateOrderAsync(order);
                return Result<Order>.Success(order);
            }
            catch (Exception ex)
            {
                return Result<Order>.Failure(new[] { "An error occurred while updating the order status.", ex.Message });
            }
        }

        public async Task<Result> RemoveLoginOrderAsync(int id)
        {
            var order = await _orderRepository.GetByIdAsync(id, true);
            if (order is null)
                return Result.Failure(new[] { "Order not found." });
            await _orderRepository.LogicDelete(id);
            await _orderRepository.SaveChangesAsync();
            return Result.Success();

        }
    }
}

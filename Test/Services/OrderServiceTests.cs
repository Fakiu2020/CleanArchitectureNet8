using Moq;
using Application.Services;
using Application.Command.Order.Create;
using FluentValidation;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Xunit;
using Domain.Interfaces;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace Test.Services
{
    public class OrderServiceTests
    {
        private readonly IOrderService _orderService;
        private readonly Mock<IOrderRepository> _orderRepositoryMock;
        private readonly Mock<IAssetService> _assetServiceMock;

        public OrderServiceTests()
        {
            _orderRepositoryMock = new Mock<IOrderRepository>();
            _assetServiceMock = new Mock<IAssetService>();
            _orderService = new OrderService(_orderRepositoryMock.Object, _assetServiceMock.Object);
        }

        [Fact]
        public async void AddNewOrderBond()
        {
            // Arrange
            var order = new Order(1, "GD30", 5, 100, 'C');
            var asset = new Asset(7, "GD30", "Bonos Globales Argentina USD Step Up 2030", AssetTypeEnum.Bond, 336.1m);

            // Configura el mock para que devuelva el asset esperado
            _assetServiceMock.Setup(a => a.GetAssetByNameAsync("GD30")).ReturnsAsync(asset);

            // Configura el mock del repositorio de órdenes
            _orderRepositoryMock.Setup(o => o.Add(It.IsAny<Order>())).ReturnsAsync(order);
            _orderRepositoryMock.Setup(o => o.SaveChangesAsync()).Returns(Task.CompletedTask);

            // Act
            var result = await _orderService.AddOrderAsync(order);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(order.Id, result.Id);
            Assert.Equal(5 * 100 * (1 + 0.002m * 1.21m), result.TotalAmount);
            Assert.Equal(5 * 100 * 0.002m, result.Commission);
            Assert.Equal(5 * 100 * 0.002m * 0.21m, result.Tax);
            _orderRepositoryMock.Verify(o => o.Add(It.IsAny<Order>()), Times.Once);

        }

        [Fact]
        public async Task AddNewOrderStock()
        {
            // Arrange
            var order = new Order(1, "AAPL", 10, 0, 'C');
            var asset = new Asset(1, "AAPL", "Apple Inc.", AssetTypeEnum.Stock, 150m);

            _assetServiceMock.Setup(a => a.GetAssetByNameAsync("AAPL")).ReturnsAsync(asset);

            _orderRepositoryMock.Setup(o => o.Add(It.IsAny<Order>())).ReturnsAsync(order);
            _orderRepositoryMock.Setup(o => o.SaveChangesAsync()).Returns(Task.CompletedTask);

            // Act
            var result = await _orderService.AddOrderAsync(order);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(order.Id, result.Id);
            Assert.Equal(10 * 150 * (1 + 0.006m * 1.21m), result.TotalAmount);
            Assert.Equal(10 * 150 * 0.006m, result.Commission);
            Assert.Equal(10 * 150 * 0.006m * 0.21m, result.Tax);

            _orderRepositoryMock.Verify(o => o.Add(It.IsAny<Order>()), Times.Once);
            _orderRepositoryMock.Verify(o => o.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task AddNewOrderFCI()
        {
            // Arrange
            var order = new Order(1, "Delta.Pesos", 10, 0.0181m, 'C');
            var asset = new Asset(8, "Delta.Pesos", "Delta Pesos Clase A", AssetTypeEnum.FCI, 0.0181m);

            _assetServiceMock.Setup(a => a.GetAssetByNameAsync("Delta.Pesos")).ReturnsAsync(asset);

            _orderRepositoryMock.Setup(o => o.Add(It.IsAny<Order>())).ReturnsAsync(order);
            _orderRepositoryMock.Setup(o => o.SaveChangesAsync()).Returns(Task.CompletedTask);

            // Act
            var result = await _orderService.AddOrderAsync(order);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(order.Id, result.Id);
            Assert.Equal(10 * 0.0181m, result.TotalAmount);
            _orderRepositoryMock.Verify(o => o.Add(It.IsAny<Order>()), Times.Once);
        }



        #region Failed
        [Fact]
        public async void ThrowPriceIsEmptyCero()
        {
            await Assert.ThrowsAnyAsync<ArgumentException>(async () =>
            {
                var order = new Order(1, "GD30", 5, 0, 'C');
                await _orderService.AddOrderAsync(order);
            });

        }

        [Fact]
        public async void ThrowQuantityIsCero()
        {
            await Assert.ThrowsAnyAsync<ArgumentException>(async () =>
            {
                var order = new Order(1, "GD30", 0, 100, 'C');
                await _orderService.AddOrderAsync(order);
            });

        }




    }
}

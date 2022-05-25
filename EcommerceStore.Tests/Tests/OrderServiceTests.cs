using EcommerceStore.Application.Exceptions;
using EcommerceStore.Application.Interfaces;
using EcommerceStore.Application.Services;
using EcommerceStore.Domain.Entities;
using EcommerceStore.Infrastructure.Persistence;
using EcommerceStore.Infrastructure.Repositories;
using EcommerceStore.Tests.MockData;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using static EcommerceStore.Application.Enums.OrderStatusEnum;

namespace EcommerceStore.Tests.Tests
{
    public class OrderServiceTests : IDisposable
    {
        //private readonly Mock<IOrderRepository> _orderRepositoryMock = new Mock<IOrderRepository>();
        //private readonly Mock<IProductRepository> productRepositoryMock = new Mock<IProductRepository>();
        private readonly Mock<IProductService> productServiceMock = new Mock<IProductService>();
        private EcommerceContext _context;

        public OrderServiceTests()
        {
        }

        private EcommerceContext GetDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<EcommerceContext>()
                    .UseInMemoryDatabase(databaseName: "EcommerceDB")
                    .Options;

            _context = new EcommerceContext(options);

            _context.Database.EnsureCreatedAsync();

            return _context;
        }

        private OrderService GetOrderService(EcommerceContext context)
        {
            var orderRepository = new OrderRepository(context);
            var productRepository = new ProductRepository(context);
            var productService = new ProductService(productRepository);

            OrderService sut = new OrderService(orderRepository, productRepository, productService);

            return sut;
        }

        [Theory]
        [InlineData(1)]
        public async Task GetOrderByIdAsync_ShouldReturnOrder_WhenOrderExists(int orderId)
        {
            // Arrange
            _context = GetDatabaseContext();

            OrderService _sut = GetOrderService(_context);

            await SeedDatabase.SeedDatabaseWithOrdersAsync(_context);
            var status = "Created";

            // Act
            var result = await _sut.GetOrderByIdAsync(orderId);

            // Assert
            Assert.Equal(orderId, result.Id);
            Assert.Equal(status, result.Status);
        }

        [Theory]
        [InlineData(1)]
        public async Task GetOrderByIdAsync_ShouldReturnOrder_WhenOrderStatusIsCorrect(int orderId)
        {
            // Arrange
            _context = GetDatabaseContext();

            OrderService _sut = GetOrderService(_context);

            var listOfOrderStatuses = Enum.GetNames(typeof(StatusesEnum)).ToList();
            var status = "Created";

            await SeedDatabase.SeedDatabaseWithOrdersAsync(_context);

            // Act
            var result = await _sut.GetOrderByIdAsync(orderId);

            // Assert
            listOfOrderStatuses.Should().Contain(result.Status);
            Assert.Equal(status, result.Status);
        }

        [Theory]
        [InlineData(1)]
        public async Task GetAllOrdersForUserAsync_ShouldReturnAllOrdersForUser_WhenUserHaveOrders(int userId)
        {
            // Arrange
            _context = GetDatabaseContext();

            OrderService _sut = GetOrderService(_context);

            await SeedDatabase.SeedDatabaseWithOrdersAsync(_context);

            // Act
            var result = await _sut.GetAllOrdersForUserAsync(userId);

            // Assert
            result.Should().HaveCount(_context.Orders.Count(o => o.UserId.Equals(userId)));
        }

        [Theory]
        [InlineData(1)]
        public async Task GetOrderByIdAsync_ShouldThrowValidationException_WhenOrderIsDeleted(int orderId)
        {
            // Arrange 
            _context = GetDatabaseContext();

            OrderService _sut = GetOrderService(_context);

            await _context.Orders.AddAsync(OrderMockData.GetOrder("Completed", true));
            await _context.SaveChangesAsync();

            // Act & Assert
            await Assert.ThrowsAsync<ValidationException>(async () => await _sut.GetOrderByIdAsync(orderId));
        }

        [Theory]
        [InlineData(3)]
        public async Task GetAllOrdersForUserAsync_ShouldThrowValidationException_WhenUserDoesntHaveOrders(int userId)
        {
            // Arrange
            _context = GetDatabaseContext();

            OrderService _sut = GetOrderService(_context);

            await SeedDatabase.SeedDatabaseWithOrdersAsync(_context);

            // Act & Assert
            await Assert.ThrowsAsync<ValidationException>(async () => await _sut.GetAllOrdersForUserAsync(userId));
        }

        [Theory]
        [InlineData(1)]
        public async Task CreateOrderAsync_WhenOrderIsSuccessfullyCreated(int userId)
        {
            // Arrange
            _context = GetDatabaseContext();
            var orderService = new Mock<OrderService>();

            var productService = new ProductService(new ProductRepository(_context));

            OrderService _sut = GetOrderService(_context);

            var orderInputModel = OrderMockData.GetOrderInputModelForCreating();

            var orderStatus = "Created";
            await SeedDatabase.SeedDatabaseWithOrdersAsync(_context);
            await SeedDatabase.SeedDatabaseWithProductsAsync(_context);

            // Act
            await _sut.CreateOrderAsync(userId, orderInputModel);
            var result = await _sut.GetOrderByIdAsync(3);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(orderStatus, result.Status);     
        }

        [Theory]
        [InlineData(1)]
        public async Task CreateOrderAsync_ThrowValidationException_WhenProductAmountExceedProductAmountInDatabase(int userId)
        {
            // Arrange
            _context = GetDatabaseContext();

            OrderService _sut = GetOrderService(_context);

            var productService = new ProductService(new ProductRepository(_context));

            var orderInputModel = OrderMockData.GetOrderInputModelWithExceedProductAmount();

            await SeedDatabase.SeedDatabaseWithOrdersAsync(_context);
            await SeedDatabase.SeedDatabaseWithProductsAsync(_context);

            // Act & Assert
            await Assert.ThrowsAsync<ValidationException>(async () => await _sut.CreateOrderAsync(userId, orderInputModel));
        }

        [Theory]
        [InlineData(1)]
        public async Task CancelOrderAsync_ThrowValidationException_WhenOrderStatusCanceled(int orderId)
        {
            // Arrange 
            _context = GetDatabaseContext();

            OrderService _sut = GetOrderService(_context);

            await _context.Orders.AddAsync(OrderMockData.GetOrder("Canceled", false));
            await _context.SaveChangesAsync();

            // Act & Assert
            await Assert.ThrowsAsync<ValidationException>(async () => await _sut.CancelOrderAsync(orderId));
        }

        [Theory]
        [InlineData(1)]
        public async Task UpdateOrderAsync_WhenStatusIsSuccessfullyUpdated(int orderId)
        {
            // Arrange
            _context = GetDatabaseContext();

            OrderService _sut = GetOrderService(_context);

            await SeedDatabase.SeedDatabaseWithOrdersAsync(_context);

            var status = "InReview";

            // Act
            await _sut.UpdateOrderAsync(orderId);

            var updatedOrder = await _sut.GetOrderByIdAsync(orderId);

            // Assert
            Assert.Equal(status, updatedOrder.Status);
        }

        [Theory]
        [InlineData(3)]
        public async Task UpdateOrderAsync_ThrowValidationException_WhenOrderDoesntExist(int orderId)
        {
            // Arrange
            _context = GetDatabaseContext();

            OrderService _sut = GetOrderService(_context);

            await SeedDatabase.SeedDatabaseWithOrdersAsync(_context);

            // Act & Assert
            await Assert.ThrowsAsync<ValidationException>(async () => await _sut.UpdateOrderAsync(orderId));
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}
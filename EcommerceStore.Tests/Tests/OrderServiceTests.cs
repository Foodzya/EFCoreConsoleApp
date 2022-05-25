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
    public class OrderServiceTests
    {
        //private readonly Mock<IOrderRepository> _orderRepositoryMock = new Mock<IOrderRepository>();
        //private readonly Mock<IProductRepository> productRepositoryMock = new Mock<IProductRepository>();
        private readonly Mock<IProductService> productServiceMock = new Mock<IProductService>();

        public OrderServiceTests()
        {
        }

        private DbContextOptions<EcommerceContext> GetDbContextOptions(string dbName)
        {
            return new DbContextOptionsBuilder<EcommerceContext>()
                    .UseInMemoryDatabase(databaseName: dbName)
                    .Options;
        }

        private OrderService GetOrderService(string dbName)
        {
            EcommerceContext _context = new EcommerceContext(GetDbContextOptions(dbName));

            var orderRepository = new OrderRepository(_context);
            var productRepository = new ProductRepository(_context);
            var productService = new ProductService(productRepository);

            OrderService sut = new OrderService(orderRepository, productRepository, productService);

            return sut;
        }

        [Theory]
        [InlineData(1)]
        public async Task GetOrderByIdAsync_ShouldReturnOrder_WhenOrderExists(int orderId)
        {
            // Arrange
            var dbName = nameof(GetOrderByIdAsync_ShouldReturnOrder_WhenOrderExists);

            OrderService _sut = GetOrderService(dbName);

            using (var context = new EcommerceContext(GetDbContextOptions(dbName)))
            {
                await SeedDatabase.SeedDatabaseWithOrdersAsync(context);
            }

            var customerFullName = "John Wick";
            var status = StatusesEnum.Created.ToString();

            // Act
            var result = await _sut.GetOrderByIdAsync(orderId);

            // Assert
            Assert.Equal(orderId, result.Id);
            Assert.Equal(status, result.Status);
            Assert.Equal(customerFullName, result.CustomerFullName);
        }

        [Theory]
        [InlineData(1)]
        public async Task GetOrderByIdAsync_ShouldReturnOrder_WhenOrderStatusIsCorrect(int orderId)
        {
            // Arrange
            var dbName = nameof(GetOrderByIdAsync_ShouldReturnOrder_WhenOrderStatusIsCorrect);

            OrderService _sut = GetOrderService(dbName);

            var listOfOrderStatuses = Enum.GetNames(typeof(StatusesEnum)).ToList();
            var status = StatusesEnum.Created.ToString();

            using (var context = new EcommerceContext(GetDbContextOptions(dbName)))
            {
                context.Orders.Add(OrderMockData.GetOrder(orderId, StatusesEnum.Created, false));
                await context.SaveChangesAsync();
            }

            // Act
            var result = await _sut.GetOrderByIdAsync(orderId);

            // Assert
            listOfOrderStatuses.Should().Contain(result.Status);
            Assert.Equal(status, result.Status);
            Assert.Equal(orderId, result.Id);
        }

        [Theory]
        [InlineData(1)]
        public async Task GetAllOrdersForUserAsync_ShouldReturnAllOrdersForUser_WhenUserHaveOrders(int userId)
        {
            // Arrange
            var dbName = nameof(GetAllOrdersForUserAsync_ShouldReturnAllOrdersForUser_WhenUserHaveOrders);

            OrderService _sut = GetOrderService(dbName);

            using (var _context = new EcommerceContext(GetDbContextOptions(dbName)))
            {
                await SeedDatabase.SeedDatabaseWithOrdersAsync(_context);
            }

            // Act
            var result = await _sut.GetAllOrdersForUserAsync(userId);

            int orderCount;

            using (var _context = new EcommerceContext(GetDbContextOptions(dbName)))
            {
                orderCount = _context.Orders.Count(o => o.UserId.Equals(userId));
            }

            // Assert
            result.Should().HaveCount(orderCount);
        }

        [Theory]
        [InlineData(1)]
        public async Task GetOrderByIdAsync_ShouldThrowValidationException_WhenOrderIsDeleted(int orderId)
        {
            // Arrange 
            var dbName = nameof(GetOrderByIdAsync_ShouldThrowValidationException_WhenOrderIsDeleted);

            OrderService _sut = GetOrderService(dbName);

            var order = OrderMockData.GetOrder(orderId, StatusesEnum.Created, true);

            // Act
            using (var context = new EcommerceContext(GetDbContextOptions(dbName)))
            {
                context.Orders.Add(order);
                await context.SaveChangesAsync();
            }

            var message = NotFoundExceptionMessages.OrderNotFound;

            // Assert
            var exception = await Assert.ThrowsAsync<ValidationException>(async () => await _sut.GetOrderByIdAsync(orderId));
            Assert.Equal(message, exception.Message);
        }

        [Theory]
        [InlineData(3)]
        public async Task GetAllOrdersForUserAsync_ShouldThrowValidationException_WhenUserDoesntHaveOrders(int userId)
        {
            // Arrange
            var dbName = nameof(GetAllOrdersForUserAsync_ShouldThrowValidationException_WhenUserDoesntHaveOrders);

            OrderService _sut = GetOrderService(dbName);

            using (var context = new EcommerceContext(GetDbContextOptions(dbName)))
            {
                await SeedDatabase.SeedDatabaseWithOrdersAsync(context);
            }

            // Act 
            var message = NotFoundExceptionMessages.OrderNotFound;

            //Assert
            var exception = await Assert.ThrowsAsync<ValidationException>(async () => await _sut.GetAllOrdersForUserAsync(userId));

            Assert.Equal(message, exception.Message);
        }

        [Theory]
        [InlineData(1)]
        public async Task CreateOrderAsync_WhenOrderIsSuccessfullyCreated(int userId)
        {
            // Arrange
            var dbName = nameof(CreateOrderAsync_WhenOrderIsSuccessfullyCreated);

            OrderService _sut = GetOrderService(dbName);

            var orderInputModel = OrderMockData.GetOrderInputModelForCreating();

            var orderStatus = StatusesEnum.Created.ToString();

            using (var context = new EcommerceContext(GetDbContextOptions(dbName)))
            {
                await SeedDatabase.SeedDatabaseWithProductsAsync(context);
            }

            // Act
            await _sut.CreateOrderAsync(userId, orderInputModel);

            Order result;

            using (var context = new EcommerceContext(GetDbContextOptions(dbName)))
            {
                result = await context.Orders.FirstOrDefaultAsync(o => o.Id == 1);
            }

            // Assert
            Assert.NotNull(result);
            Assert.Equal(orderStatus, result.Status);
        }

        [Theory]
        [InlineData(1)]
        public async Task CreateOrderAsync_ThrowValidationException_WhenProductAmountExceedProductAmountInDatabase(int userId)
        {
            // Arrange
            var dbName = nameof(CreateOrderAsync_ThrowValidationException_WhenProductAmountExceedProductAmountInDatabase);

            OrderService _sut = GetOrderService(dbName);

            var orderInputModel = OrderMockData.GetOrderInputModelWithExceedProductAmount();

            using (var context = new EcommerceContext(GetDbContextOptions(dbName)))
            {
                await SeedDatabase.SeedDatabaseWithOrdersAsync(context);
                await SeedDatabase.SeedDatabaseWithProductsAsync(context);
            }

            // Act & Assert
            var message = ExceptionMessages.OrderCreateFailed;

            var exception = await Assert.ThrowsAsync<ValidationException>(async () => await _sut.CreateOrderAsync(userId, orderInputModel));

            Assert.Equal(message, exception.Message);
        }

        [Theory]
        [InlineData(1)]
        public async Task CancelOrderAsync_ThrowValidationException_WhenOrderStatusCanceled(int orderId)
        {
            // Arrange 
            var dbName = nameof(CancelOrderAsync_ThrowValidationException_WhenOrderStatusCanceled);

            OrderService _sut = GetOrderService(dbName);

            Order order = OrderMockData.GetOrder(orderId, StatusesEnum.Canceled, false);

            using (var context = new EcommerceContext(GetDbContextOptions(dbName)))
            {
                context.Orders.Add(order);
                await context.SaveChangesAsync();
            }

            // Act
            var message = NotFoundExceptionMessages.OrderNotFound;

            // Assert
            // check validation exception message
            var exception = await Assert.ThrowsAsync<ValidationException>(async () => await _sut.CancelOrderAsync(orderId));

            Assert.Equal(message, exception.Message);
        }

        [Theory]
        [InlineData(1)]
        public async Task UpdateOrderAsync_WhenStatusIsSuccessfullyUpdated(int orderId)
        {
            // Arrange
            var dbName = nameof(UpdateOrderAsync_WhenStatusIsSuccessfullyUpdated);

            OrderService _sut = GetOrderService(dbName);

            using (var context = new EcommerceContext(GetDbContextOptions(dbName)))
            {
                await SeedDatabase.SeedDatabaseWithOrdersAsync(context);
            }

            var status = StatusesEnum.InReview.ToString();

            // Act
            await _sut.UpdateOrderAsync(orderId);

            Order updatedOrder;

            using (var context = new EcommerceContext(GetDbContextOptions(dbName)))
            {
                updatedOrder = await context.Orders.FirstOrDefaultAsync(o => o.Id == orderId);
            }

            // Assert
            Assert.Equal(status, updatedOrder.Status);
        }

        [Theory]
        [InlineData(3)]
        public async Task UpdateOrderAsync_ThrowValidationException_WhenOrderDoesntExist(int orderId)
        {
            // Arrange
            var dbName = nameof(UpdateOrderAsync_ThrowValidationException_WhenOrderDoesntExist);

            OrderService _sut = GetOrderService(dbName);

            using (var context = new EcommerceContext(GetDbContextOptions(dbName)))
            {
                await SeedDatabase.SeedDatabaseWithOrdersAsync(context);
            }

            // Act & Assert
            var message = NotFoundExceptionMessages.OrderNotFound;

            var exception = await Assert.ThrowsAsync<ValidationException>(async () => await _sut.UpdateOrderAsync(orderId));

            Assert.Equal(message, exception.Message);
        }
    }
}
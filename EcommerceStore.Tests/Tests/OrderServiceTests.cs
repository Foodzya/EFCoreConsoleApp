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
        private readonly EcommerceContext _context;
        private OrderService _sut;
        //private readonly Mock<IOrderRepository> _orderRepositoryMock = new Mock<IOrderRepository>();
        //private readonly Mock<IProductRepository> productRepositoryMock = new Mock<IProductRepository>();
        private readonly Mock<IProductService> productServiceMock = new Mock<IProductService>();

        public OrderServiceTests()
        {
            var options = new DbContextOptionsBuilder<EcommerceContext>()
                .UseInMemoryDatabase(databaseName: "EcommerceDB")
                .Options;

            _context = new EcommerceContext(options);

            _context.Database.EnsureCreatedAsync();
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task GetOrderByIdAsync_ShouldReturnOrder_WhenOrderExists(int orderId)
        {
            // Arrange
            _sut = new OrderService(new OrderRepository(_context), new ProductRepository(_context), productServiceMock.Object);

            await SeedDatabaseWithOrdersAsync();

            // Act
            var result = await _sut.GetOrderByIdAsync(orderId);

            // Assert
            Assert.Equal(orderId, result.Id);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task GetOrderByIdAsync_ShouldReturnOrder_WhenOrderStatusIsCorrect(int orderId)
        {
            // Arrange
            _sut = new OrderService(new OrderRepository(_context), new ProductRepository(_context), productServiceMock.Object);
            var listOfOrderStatuses = Enum.GetNames(typeof(StatusesEnum)).ToList();

            await SeedDatabaseWithOrdersAsync();

            // Act
            var result = await _sut.GetOrderByIdAsync(orderId);

            // Assert
            listOfOrderStatuses.Should().Contain(result.Status);

        }

        [Theory]
        [InlineData(1)]
        public async Task GetAllOrdersForUserAsync_ShouldReturnAllOrdersForUser_WhenUserHaveOrders(int userId)
        {
            // Arrange
            _sut = new OrderService(new OrderRepository(_context), new ProductRepository(_context), productServiceMock.Object);

            await SeedDatabaseWithOrdersAsync();

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
            _sut = new OrderService(new OrderRepository(_context), new ProductRepository(_context), productServiceMock.Object);

            await _context.Orders.AddAsync(OrderMockData.GetOrderWithIsDeletedTrue());
            await _context.SaveChangesAsync();

            // Act & Assert
            await Assert.ThrowsAsync<ValidationException>(async () => await _sut.GetOrderByIdAsync(orderId));
        }

        [Theory]
        [InlineData(3)]
        public async Task GetAllOrdersForUserAsync_ShouldThrowValidationException_WhenUserDoesntHaveOrders(int userId)
        {
            // Arrange
            _sut = new OrderService(new OrderRepository(_context), new ProductRepository(_context), productServiceMock.Object);
            await SeedDatabaseWithOrdersAsync();

            // Act & Assert
            await Assert.ThrowsAsync<ValidationException>(async () => await _sut.GetAllOrdersForUserAsync(userId));
        }

        [Theory]
        [InlineData(1)]
        public async Task CreateOrderAsync_WhenOrderIsSuccessfullyCreated(int userId)
        {
            // Arrange
            var orderService = new Mock<OrderService>();

            var productService = new ProductService(new ProductRepository(_context));

            _sut = new OrderService(new OrderRepository(_context), new ProductRepository(_context), productService);

            var orderInputModel = OrderMockData.GetOrderInputModelForCreating();

            await SeedDatabaseWithOrdersAsync();
            await SeedDatabaseWithProductsAsync();

            // Act
            await _sut.CreateOrderAsync(userId, orderInputModel);
            var result = await _sut.GetOrderByIdAsync(3);

            // Assert
            Assert.NotNull(result);
        }

        [Theory]
        [InlineData(1)]
        public async Task CancelOrderAsync_ThrowValidationException_WhenOrderStatusCanceled(int orderId)
        {
            // Arrange 
            _sut = new OrderService(new OrderRepository(_context), new ProductRepository(_context), productServiceMock.Object);

            await _context.Orders.AddAsync(OrderMockData.GetOrderWithCanceledStatus());
            await _context.SaveChangesAsync();

            // Act & Assert
            await Assert.ThrowsAsync<ValidationException>(async () => await _sut.CancelOrderAsync(orderId));
        }

        private async Task SeedDatabaseWithOrdersAsync()
        {
            await _context.Orders.AddRangeAsync(new List<Order>
            {
                new Order
                {
                    Id = 1,
                    IsDeleted = false,
                    ModifiedDate = DateTime.UtcNow,
                    Status = "Created",
                    UserId = 2,
                    User = new User
                    {
                        Id = 2,
                        FirstName = "John",
                        LastName = "Wick",
                        Email = "emailfortest@gmail.com",
                        PhoneNumber = "13371337"
                    },
                    ProductOrders = new List<ProductOrder>()
                },
                new Order
                {
                    Id = 2,
                    IsDeleted = false,
                    ModifiedDate = DateTime.UtcNow,
                    Status = "InDelivery",
                    UserId = 1,
                    User = new User()
                    {
                        Id = 1,
                        FirstName = "Alex",
                        LastName = "Bordson",
                        Email = "secondemailfortest@mail.ru",
                        PhoneNumber = "133712345"
                    },
                    ProductOrders = new List<ProductOrder>()
                }
            });

            await _context.SaveChangesAsync();
        }

        private async Task SeedDatabaseWithProductsAsync()
        {
            await _context.Products.AddRangeAsync(new List<Product>
            {
                new Product
                {
                    Id = 1,
                    Name = "Adidas Ozweego",
                    Image = "someimage",
                    Description = "Some description",
                    Price = 100,
                    Quantity = 10,
                    ProductCategoryId = 1,
                    BrandId = 1,
                    Brand = new Brand
                    {
                        Id = 1,
                        FoundationYear = 1990,
                        Name = "Some brand",
                        IsDeleted = false
                    },
                    ProductOrders = new List<ProductOrder>()
                    {
                        new ProductOrder
                        {
                            Id = 2,
                            Quantity = 20,
                            Price = 200
                        }
                    },
                    ProductCategory = new ProductCategory()
                    {
                        Id = 2, 
                        Name = "Something else"
                    }
                },
                new Product
                {
                    Id = 2,
                    Name = "Adidas Superstar",
                    Image = "someimageofsneakers",
                    Description = "Some description",
                    Price = 120,
                    Quantity = 10,
                    ProductCategoryId = 1,
                    BrandId = 1,
                    ProductCategory = new ProductCategory()
                    {
                        Id = 1,
                        Name = "Something"
                    },
                    ProductOrders = new List<ProductOrder>()
                    {
                        new ProductOrder
                        {
                            Id = 1,
                            Quantity = 10,
                            Price = 150
                        }
                    },
                    Brand = new Brand
                    {
                        Id = 2,
                        FoundationYear = 1995,
                        Name = "Some new brand",
                        IsDeleted = false
                    }
                }
            });

            await _context.SaveChangesAsync();
        }



        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}
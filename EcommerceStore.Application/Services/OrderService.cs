using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcommerceStore.Application.Exceptions;
using EcommerceStore.Application.Interfaces;
using EcommerceStore.Application.Models.InputModels;
using EcommerceStore.Application.Models.ViewModels;
using EcommerceStore.Domain.Entities;
using EcommerceStore.Domain.Interfaces;

namespace EcommerceStore.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;

        public OrderService(IOrderRepository orderRepository, IProductRepository productRepository)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
        }

        public async Task CreateOrderAsync(int userId, OrderInputModel orderInputModel)
        {

            var order = new Order
            {
                ModifiedDate = DateTime.UtcNow,
                Status = "Created",
                UserId = userId,
                ProductOrders = new List<ProductOrder>()
            };

            await _orderRepository.CreateAsync(order);

            await _orderRepository.SaveChangesAsync();

            var orderId = order.Id;

            foreach (var productDetail in orderInputModel.ProductsDetails)
            {
                var product = await _productRepository.GetByIdAsync(productDetail.ProductId);

                if (productDetail.ProductAmount <= product.Quantity && product != null)
                {
                    order.ProductOrders.Add(new ProductOrder
                    {
                        OrderId = orderId,
                        ProductId = productDetail.ProductId,
                        Quantity = productDetail.ProductAmount,
                        Price = product.Price * productDetail.ProductAmount
                    });

                    product.Quantity -= productDetail.ProductAmount;
                }
                else
                {
                    order.Status = "Failed";

                    await _orderRepository.SaveChangesAsync();

                    throw new Exception("Products out of stock");
                }
            }

            await _orderRepository.SaveChangesAsync();
            await _productRepository.SaveChangesAsync();
        }

        public async Task<List<OrderViewModel>> GetAllOrdersForUserAsync(int userId)
        {
            var orders = await _orderRepository.GetAllForUserAsync(userId);

            if (orders is null)
                throw new ValidationException(NotFoundExceptionMessages.OrderNotFound);

            var ordersViewModel = orders
                .Select(o => new OrderViewModel
                {
                    Id = o.Id,
                    Status = o.Status,
                    CustomerFullName = $"{o.User.FirstName} {o.User.LastName}",
                    Products = o.ProductOrders.Select(p => new ProductViewModel()
                    {
                        Id = p.Id,
                        Name = p.Product.Name,
                        BrandName = p.Product.Brand.Name,
                        Description = p.Product.Description,
                        Image = p.Product.Image,
                        Price = p.Product.Price,
                        Quantity = p.Quantity
                    }).ToList(),
                    TotalPrice = o.ProductOrders
                        .Sum(p => p.Price)
                })
                .ToList();

            return ordersViewModel;
        }

        public async Task<OrderViewModel> GetOrderByIdAsync(int orderId)
        {
            var order = await _orderRepository.GetByIdAsync(orderId);

            if (order is null)
                throw new ValidationException(NotFoundExceptionMessages.OrderNotFound, orderId);

            var orderViewModel = new OrderViewModel
            {
                Id = order.Id,
                Status = order.Status,
                CustomerFullName = $"{order.User.FirstName} {order.User.LastName}",
                Products = order.ProductOrders.Select(p => new ProductViewModel()
                {
                    Id = p.Id,
                    Name = p.Product.Name,
                    BrandName = p.Product.Brand.Name,
                    Description = p.Product.Description,
                    Image = p.Product.Image,
                    Price = p.Product.Price,
                    Quantity = p.Quantity
                }).ToList(),
                TotalPrice = order.ProductOrders
                        .Sum(p => p.Price)
            };

            return orderViewModel;
        }

        public async Task CancelOrderAsync(int orderId)
        {
            var order = await _orderRepository.GetByIdAsync(orderId);

            if (order is null)
                throw new ValidationException(NotFoundExceptionMessages.OrderNotFound, orderId);

            order.Status = "Canceled";

            _orderRepository.Update(order);

            await _orderRepository.SaveChangesAsync();
        }

        public async Task RemoveProductFromOrderAsync(int orderId, int productId)
        {
            await _orderRepository.RemoveProductFromOrderAsync(orderId, productId);
        }

        public Task SubmitOrderAsync(int orderId)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateOrderAsync(int orderId)
        {
            var order = await _orderRepository.GetByIdAsync(orderId);

            if (order is null)
                throw new ValidationException(NotFoundExceptionMessages.OrderNotFound, orderId);

            order.ModifiedDate = DateTime.UtcNow;

            switch (order.Status)
            {
                case "Created":
                    order.Status = "In Review";
                    break;
                case "In Review":
                    order.Status = "In Delivery";
                    break;
                case "In Delivery":
                    order.Status = "Completed";
                    break;
                default:
                    throw new Exception($"Order {orderId} failed");
            }

            _orderRepository.Update(order);

            await _orderRepository.SaveChangesAsync();
        }

        public async Task AddProductsToOrderAsync(int orderId, OrderInputModel orderInputModel)
        {
            var order = await _orderRepository.GetByIdAsync(orderId);

            foreach (var productDetail in orderInputModel.ProductsDetails)
            {
                var product = await _productRepository.GetByIdAsync(productDetail.ProductId);

                if (productDetail.ProductAmount <= product.Quantity && product != null)
                {
                    order.ProductOrders.Add(new ProductOrder
                    {
                        OrderId = orderId,
                        ProductId = productDetail.ProductId,
                        Quantity = productDetail.ProductAmount,
                        Price = product.Price * productDetail.ProductAmount
                    });

                    product.Quantity -= productDetail.ProductAmount;
                }
                else
                {
                    throw new Exception("Products out of stock");
                }
            }

            await _orderRepository.SaveChangesAsync();
        }
    }
}
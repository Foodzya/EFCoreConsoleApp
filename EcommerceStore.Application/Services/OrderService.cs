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
using EcommerceStore.Infrastructure.Repositories;

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

        public async Task CreateOrderAsync(OrderInputModel orderInputModel)
        {
            var order = new Order
            {
                ModifiedDate = DateTime.UtcNow,
                Status = "Created",
                UserId = orderInputModel.UserId
            };

            var orders = await _orderRepository.GetAllAsync();

            List<Product> products = new List<Product>();

            foreach(var productId in orderInputModel.ProductsIds)
            {
                products.Add(await _productRepository.GetByIdAsync(productId));
            }

            await _orderRepository.CreateAsync(order);

            await _orderRepository.SaveChangesAsync();
        }

        public async Task<List<OrderViewModel>> GetAllOrdersForUserAsync(int userId)
        {
            var orders = await _orderRepository.GetAllAsync(userId);

            if (orders is null)
                throw new ValidationException(NotFoundExceptionMessages.OrderNotFound);

            var ordersViewModel = orders
                .Select(o => new OrderViewModel
                {
                    Status = o.Status,
                    CustomerFullName = $"{o.User.FirstName} {o.User.LastName}",
                    Products = new List<ProductViewModel>
                    {
                        
                    },
                    TotalPrice = o.ProductOrders
                        .Sum(p => p.Price)
                })
                .ToList();

            return ordersViewModel;
        }

        public async Task<OrderViewModel> GetOrderByIdAsync(int orderId)
        {
            var order = await _orderRepository.GetByIdAsync(orderId);

            if (order is null || order.IsDeleted)
                throw new ValidationException(NotFoundExceptionMessages.OrderNotFound, orderId);

            var orderViewModel = new OrderViewModel
            {
                Status = order.Status,
                CustomerFullName = $"{order.User.FirstName} {order.User.LastName}",
                ProductNames = order.ProductOrders
                        .Where(p => p.OrderId == order.Id)
                        .Select(p => p.Product.Name)
                        .ToList(),
                TotalPrice = order.ProductOrders
                        .Where(p => p.OrderId == order.Id)
                        .Sum(p => p.Price)
            };

            return orderViewModel;
        }

        public async Task RemoveOrderAsync(int orderId)
        {
            var order = await _orderRepository.GetByIdAsync(orderId);

            if (order is null)
                throw new ValidationException(NotFoundExceptionMessages.OrderNotFound, orderId);

            order.IsDeleted = true;

            _orderRepository.Update(order);

            await _orderRepository.SaveChangesAsync();
        }

        public async Task UpdateOrderAsync(int orderId, OrderInputModel orderInputModel)
        {
            var order = await _orderRepository.GetByIdAsync(orderId);

            if (order is null || order.IsDeleted)
                throw new ValidationException(NotFoundExceptionMessages.OrderNotFound, orderId);

            order.ModifiedDate = orderInputModel.ModifiedDate;
            order.Status = orderInputModel.Status;
            order.UserId = orderInputModel.UserId;

            _orderRepository.Update(order);

            await _orderRepository.SaveChangesAsync();
        }
    }
}
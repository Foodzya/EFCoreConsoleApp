using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcommerceStore.Application.Enums;
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
        private readonly IProductService _productService;

        public OrderService(IOrderRepository orderRepository, IProductRepository productRepository, IProductService productService)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _productService = productService;
        }

        public async Task CreateOrderAsync(int userId, OrderInputModel orderInputModel)
        {

            var order = new Order
            {
                ModifiedDate = DateTime.UtcNow,
                Status = OrderStatusEnum.StatusesEnum.Created.ToString(),
                UserId = userId,
                ProductOrders = new List<ProductOrder>()
            };

            var productIds = orderInputModel.ProductsDetails.Select(p => p.ProductId).ToList();

            var productsForOrder = await _productRepository.GetByIdsAsync(productIds);

            foreach (var productDetail in orderInputModel.ProductsDetails)
            {
                var product = productsForOrder.FirstOrDefault(p => p.Id == productDetail.ProductId);

                if (await _productService.IsProductAvailableInStockAsync(product.Id, productDetail.ProductAmount))
                {
                    order.ProductOrders.Add(new ProductOrder
                    {
                        Order = order,
                        ProductId = productDetail.ProductId,
                        Quantity = productDetail.ProductAmount,
                        Price = product.Price * productDetail.ProductAmount
                    });

                    product.Quantity -= productDetail.ProductAmount;
                }
                else
                {
                    order.Status = OrderStatusEnum.StatusesEnum.Failed.ToString();

                    await _orderRepository.SaveChangesAsync();
                }
            }

            await _orderRepository.CreateAsync(order);

            if (order.Status == OrderStatusEnum.StatusesEnum.Failed.ToString())
            {
                throw new ValidationException(ExceptionMessages.OrderCreateFailed);
            }            

            await _orderRepository.SaveChangesAsync();
            await _productRepository.SaveChangesAsync();
        }

        public async Task<List<OrderViewModel>> GetAllOrdersForUserAsync(int userId)
        {
            var orders = await _orderRepository.GetAllForUserAsync(userId);

            if (orders.Count == 0)
                throw new ValidationException(NotFoundExceptionMessages.OrderNotFound);

            var ordersViewModel = orders
                .Select(o => new OrderViewModel
                {
                    Id = o.Id,
                    Status = o.Status,
                    CustomerFullName = $"{o.User.FirstName} {o.User.LastName}",
                    Products = o.ProductOrders.Select(p => new ProductViewModel()
                    {
                        Id = p.Product.Id,
                        Name = p.Product.Name,
                        BrandName = p.Product.Brand.Name,
                        Description = p.Product.Description,
                        Image = p.Product.Image,
                        Price = p.Product.Price,
                        Quantity = p.Quantity
                    }).ToList(),
                    TotalPrice = o.ProductOrders.Sum(p => p.Price)
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
                    Id = p.Product.Id,
                    Name = p.Product.Name,
                    BrandName = p.Product.Brand.Name,
                    Description = p.Product.Description,
                    Image = p.Product.Image,
                    Price = p.Product.Price,
                    Quantity = p.Quantity
                }).ToList(),
                TotalPrice = order.ProductOrders.Sum(p => p.Price)
            };

            return orderViewModel;
        }

        public async Task CancelOrderAsync(int orderId)
        {
            var order = await _orderRepository.GetByIdAsync(orderId);

            if (order is null || order.Status == OrderStatusEnum.StatusesEnum.Canceled.ToString() || order.Status == OrderStatusEnum.StatusesEnum.Failed.ToString())
                throw new ValidationException(NotFoundExceptionMessages.OrderNotFound, orderId);

            order.Status = OrderStatusEnum.StatusesEnum.Canceled.ToString();

            var productsInOrder = order.ProductOrders
                .Select(p => new
                {
                    ProductId = p.ProductId,
                    ProductQuantity = p.Quantity
                })
                .ToList();

            var products = await _productRepository.GetByIdsAsync(productsInOrder.Select(p => p.ProductId).ToList());

            products.ForEach(p =>
            {
                productsInOrder.ForEach(o =>
                {
                    if (p.Id == o.ProductId)
                        p.Quantity += o.ProductQuantity;
                });
            });

            _orderRepository.Update(order);

            await _productRepository.SaveChangesAsync();
            await _orderRepository.SaveChangesAsync();
        }

        public async Task RemoveProductFromOrderAsync(int orderId, int productId)
        {
            var order = await _orderRepository.GetByIdAsync(orderId);

            var productQuantity = order.ProductOrders.FirstOrDefault(o => o.ProductId == productId && o.OrderId == orderId).Quantity;

            await _orderRepository.RemoveProductFromOrderAsync(orderId, productId);

            var product = await _productRepository.GetByIdAsync(productId);

            product.Quantity += productQuantity;

            await _productRepository.SaveChangesAsync();
            await _orderRepository.SaveChangesAsync();
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

            var status = order.Status;

            switch (status)
            {
                case "Created":
                    order.Status = OrderStatusEnum.StatusesEnum.InReview.ToString();
                    break;
                case "InReview":
                    order.Status = OrderStatusEnum.StatusesEnum.InDelivery.ToString();
                    break;
                case "InDelivery":
                    order.Status = OrderStatusEnum.StatusesEnum.Completed.ToString();
                    break;
                default:
                    throw new ValidationException(ExceptionMessages.OrderUpdateFailed, orderId);
            }

            _orderRepository.Update(order);

            await _orderRepository.SaveChangesAsync();
        }

        public async Task AddProductsToOrderAsync(int orderId, OrderInputModel orderInputModel)
        {
            var order = await _orderRepository.GetByIdAsync(orderId);

            var productIds = orderInputModel.ProductsDetails
                .Select(p => p.ProductId)
                .ToList();

            var productsForOrder = await _productRepository.GetByIdsAsync(productIds);

            foreach (var productDetail in orderInputModel.ProductsDetails)
            {
                var product = productsForOrder.FirstOrDefault(p => p.Id == productDetail.ProductId);

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
            await _productRepository.SaveChangesAsync();
        }
    }
}
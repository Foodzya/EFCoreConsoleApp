using System.Collections.Generic;
using System.Threading.Tasks;
using EcommerceStore.Application.Models.InputModels;
using EcommerceStore.Application.Models.ViewModels;

namespace EcommerceStore.Application.Interfaces
{
    public interface IOrderService
    {
        public Task<OrderViewModel> GetOrderByIdAsync(int orderId);
        public Task<List<OrderViewModel>> GetAllOrdersForUserAsync(int userId);
        public Task CreateOrderAsync(int userId, OrderInputModel orderInputModel);
        public Task AddProductsToOrderAsync(int orderId, OrderInputModel orderInputModel);
        public Task CancelOrderAsync(int orderId);
        public Task UpdateOrderAsync(int orderId, OrderInputModel orderInputModel);
        public Task RemoveProductFromOrderAsync(int orderId, int productId);
        public Task SubmitOrderAsync(int orderId);
    }
}
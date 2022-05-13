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
        public Task CreateOrderAsync(OrderInputModel orderInputModel);
        public Task RemoveOrderAsync(int orderId);
        public Task UpdateOrderAsync(int orderId, OrderInputModel orderInputModel);
    }
}
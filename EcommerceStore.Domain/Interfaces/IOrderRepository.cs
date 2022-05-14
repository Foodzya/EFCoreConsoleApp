using System.Collections.Generic;
using System.Threading.Tasks;
using EcommerceStore.Domain.Entities;

namespace EcommerceStore.Domain.Interfaces
{
    public interface IOrderRepository
    {
        public Task<Order> GetByIdAsync(int orderId);
        public Task<List<Order>> GetAllForUserAsync(int userId);
        public Task CreateAsync(Order order);
        public Task RemoveProductFromOrderAsync(int orderId, int productId);
        public void Remove(Order order);
        public void Update(Order order);
        public Task SaveChangesAsync();
    }
}
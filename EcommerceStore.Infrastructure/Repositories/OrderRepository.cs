using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcommerceStore.Domain.Entities;
using EcommerceStore.Domain.Interfaces;
using EcommerceStore.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EcommerceStore.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly EcommerceContext _context;

        public OrderRepository(EcommerceContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
        }

        public async Task<List<Order>> GetAllForUserAsync(int userId)
        {
            var orders = await _context.Orders
                .Include(o => o.User)
                .Include(o => o.ProductOrders)
                    .ThenInclude(p => p.Product)
                        .ThenInclude(p => p.Brand)
                .Where(o => !o.IsDeleted && o.UserId == userId)
                .ToListAsync();

            return orders;
        }

        public async Task<Order> GetByIdAsync(int orderId)
        {
            var order = await _context.Orders
                .Include(o => o.User)
                .Include(o => o.ProductOrders)
                    .ThenInclude(p => p.Product)
                        .ThenInclude(p => p.Brand)
                .Where(o => !o.IsDeleted)
                .FirstOrDefaultAsync(o => o.Id == orderId);

            return order;
        }

        public void Remove(Order order)
        {
            _context.Orders.Remove(order);
        }

        public async Task RemoveProductFromOrderAsync(int orderId, int productId)
        {
            var productorder = await _context.ProductOrders.FirstOrDefaultAsync(p => p.OrderId == orderId && p.ProductId == productId);

            _context.ProductOrders.Remove(productorder);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Update(Order order)
        {
            _context.Orders.Update(order);
        }
    }
}

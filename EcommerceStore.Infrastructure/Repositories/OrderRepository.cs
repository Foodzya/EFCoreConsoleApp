using System.Collections.Generic;
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

        public async Task<List<Order>> GetAllAsync()
        {
            var orders = await _context.Orders
                .Include(o => o.User)
                .ToListAsync();

            return orders;
        }

        public async Task<Order> GetByIdAsync(int orderId)
        {
            var order = await _context.Orders
                .Include(o => o.User)
                .FirstOrDefaultAsync(o => o.Id == orderId);

            return order;
        }

        public void Remove(Order order)
        {
            _context.Orders.Remove(order);
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

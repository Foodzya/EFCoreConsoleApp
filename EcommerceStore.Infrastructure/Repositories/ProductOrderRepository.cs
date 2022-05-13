using System.Collections.Generic;
using System.Threading.Tasks;
using EcommerceStore.Domain.Entities;
using EcommerceStore.Infrastructure.Persistence;

namespace EcommerceStore.Infrastructure.Repositories
{
    public class ProductOrderRepository
    {
        private readonly EcommerceContext _context;

        public ProductOrderRepository(EcommerceContext context)
        {
            _context = context;
        }

        public async Task CreateProductOrderAsync(ProductOrder productOrder)
        {
            await _context.ProductOrders.AddAsync(productOrder);
        }
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using EcommerceStore.Domain.Entities;
using EcommerceStore.Domain.Interfaces;
using EcommerceStore.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EcommerceStore.Infrastructure.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly EcommerceContext _context;

        public ReviewRepository(EcommerceContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Review review)
        {
            await _context.Reviews.AddAsync(review);
        }

        public async Task<List<Review>> GetAllAsync()
        {
            var reviews = await _context.Reviews
                .Include(r => r.User)
                .Include(r => r.Product)
                .ToListAsync();

            return reviews;
        }

        public async Task<Review> GetByIdAsync(int reviewId)
        {
            var review = await _context.Reviews
                .Include(r => r.User)
                .Include(r => r.Product)
                .FirstOrDefaultAsync(r => r.Id == reviewId);

            return review;
        }

        public void Remove(Review review)
        {
            _context.Remove(review);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Update(Review review)
        {
            _context.Reviews.Update(review);
        }
    }
}
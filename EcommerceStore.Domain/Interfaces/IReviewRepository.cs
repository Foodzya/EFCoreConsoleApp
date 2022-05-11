using System.Collections.Generic;
using System.Threading.Tasks;
using EcommerceStore.Domain.Entities;

namespace EcommerceStore.Domain.Interfaces
{
    public interface IReviewRepository
    {
        public Task<Review> GetByIdAsync(int reviewId);
        public Task<List<Review>> GetAllAsync();
        public Task CreateAsync(Review review);
        public void Remove(Review review);
        public void Update(Review review);
        public Task SaveChangesAsync();
    }
}
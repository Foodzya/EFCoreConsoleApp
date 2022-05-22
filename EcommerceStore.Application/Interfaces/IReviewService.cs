using System.Threading.Tasks;
using System.Collections.Generic;
using EcommerceStore.Application.Models.InputModels;
using EcommerceStore.Application.Models.ViewModels;

namespace EcommerceStore.Application.Interfaces
{
    public interface IReviewService
    {
        public Task<List<ReviewViewModel>> GetAllReviewsAsync();
        public Task<ReviewViewModel> GetReviewByIdAsync(int reviewId);
        public Task CreateReviewAsync(ReviewInputModel reviewInputModel);
        public Task UpdateReviewAsync(int reviewId, ReviewInputModel reviewInputModel);
        public Task RemoveReviewAsync(int reviewId);
    }
}
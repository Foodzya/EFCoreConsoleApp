using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcommerceStore.Application.Exceptions;
using EcommerceStore.Application.Interfaces;
using EcommerceStore.Application.Models.InputModels;
using EcommerceStore.Application.Models.ViewModels;
using EcommerceStore.Domain.Entities;
using EcommerceStore.Domain.Interfaces;

namespace EcommerceStore.Application.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;

        public ReviewService(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public async Task CreateReviewAsync(ReviewInputModel reviewInputModel)
        {
            var review = new Review
            {
                Comment = reviewInputModel.Comment,
                Rating = reviewInputModel.Rating,
                ProductId = reviewInputModel.ProductId,
                UserId = reviewInputModel.UserId
            };

            await _reviewRepository.CreateAsync(review);

            await _reviewRepository.SaveChangesAsync();
        }

        public async Task<List<ReviewViewModel>> GetAllReviewsAsync()
        {
            var reviews = await _reviewRepository.GetAllAsync();

            if (reviews is null)
                throw new ValidationException(NotFoundExceptionMessages.ReviewNotFound);

            var reviewsViewModel = reviews
                .Select(r => new ReviewViewModel
                {
                    Comment = r.Comment,
                    Rating = r.Rating,
                    ProductName = r.Product.Name,
                    CustomerFullName = $"{r.User.FirstName} {r.User.LastName}"
                })
                .ToList();

            return reviewsViewModel;
        }

        public async Task<ReviewViewModel> GetReviewByIdAsync(int reviewId)
        {
            var review = await _reviewRepository.GetByIdAsync(reviewId);

            if (review is null)
                throw new ValidationException(NotFoundExceptionMessages.ReviewNotFound, reviewId);

            var reviewViewModel = new ReviewViewModel
            {
                Comment = review.Comment,
                Rating = review.Rating,
                ProductName = review.Product.Name,
                CustomerFullName = $"{review.User.FirstName} {review.User.LastName}"
            };

            return reviewViewModel;
        }

        public async Task RemoveReviewAsync(int reviewId)
        {
            var review = await _reviewRepository.GetByIdAsync(reviewId);

            if (review is null)
                throw new ValidationException(NotFoundExceptionMessages.ReviewNotFound, reviewId);

            _reviewRepository.Remove(review);

            await _reviewRepository.SaveChangesAsync();
        }

        public async Task UpdateReviewAsync(int reviewId, ReviewInputModel reviewInputModel)
        {
            var review = await _reviewRepository.GetByIdAsync(reviewId);

            if (review is null)
                throw new ValidationException(NotFoundExceptionMessages.ReviewNotFound, reviewId);

            review.Rating = reviewInputModel.Rating;
            review.Comment = reviewInputModel.Comment;
            review.UserId = reviewInputModel.UserId;
            review.ProductId = reviewInputModel.ProductId;

            _reviewRepository.Update(review);

            await _reviewRepository.SaveChangesAsync();
        }
    }
}
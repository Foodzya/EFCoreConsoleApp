using EcommerceStore.Application.Exceptions;
using EcommerceStore.Application.Interfaces;
using EcommerceStore.Application.Models.InputModels;
using EcommerceStore.Application.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EcommerceStore.API.Controllers
{
    [ApiController]
    [Route("api/reviews")]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewsController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ReviewViewModel>>> GetAllAsync()
        {
            var reviewsViewModel = await _reviewService.GetAllReviewsAsync();

            return Ok(reviewsViewModel);
        }

        [HttpGet("{reviewId}")]
        public async Task<ActionResult<ReviewViewModel>> GetByIdAsync([FromRoute] int reviewId)
        {
            var reviewViewModel = await _reviewService.GetReviewByIdAsync(reviewId);

            return Ok(reviewViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> CreateAsync([FromBody] ReviewInputModel reviewInputModel)
        {
            if (!ModelState.IsValid)
                throw new ValidationException(ModelState);

            await _reviewService.CreateReviewAsync(reviewInputModel);

            return Ok();
        }

        [HttpPut("{reviewId}")]
        public async Task<ActionResult> UpdateAsync([FromRoute] int reviewId, [FromBody] ReviewInputModel reviewInputModel)
        {
            if (!ModelState.IsValid)
                throw new ValidationException(ModelState);

            await _reviewService.UpdateReviewAsync(reviewId, reviewInputModel);

            return Ok();
        }

        [HttpDelete("{reviewId}")]
        public async Task<ActionResult> RemoveAsync([FromRoute] int reviewId)
        {
            await _reviewService.RemoveReviewAsync(reviewId);

            return Ok();
        }
    }
}
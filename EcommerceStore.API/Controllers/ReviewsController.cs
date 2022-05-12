using EcommerceStore.Application.Interfaces;
using EcommerceStore.Application.Models.InputModels;
using EcommerceStore.Application.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EcommerceStore.API.Controllers
{
    /// <summary>
    /// Review controller for managing reviews using CRUD operations
    /// </summary>
    [ApiController]
    [Route("api/reviews")]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewsController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        /// <summary>
        /// Get list of reviews
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Returns when list of reviews is successfully obtained</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<ReviewViewModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<ReviewViewModel>>> GetAllAsync()
        {
            var reviewsViewModel = await _reviewService.GetAllReviewsAsync();

            return Ok(reviewsViewModel);
        }

        /// <summary>
        /// Get a single review
        /// </summary>
        /// <param name="reviewId"></param>
        /// <returns></returns>
        /// <response code="200">Returns when review is successfully obtained</response>
        [HttpGet("{reviewId}")]
        [ProducesResponseType(typeof(ReviewViewModel), StatusCodes.Status200OK)]
        public async Task<ActionResult<ReviewViewModel>> GetByIdAsync([FromRoute] int reviewId)
        {
            var reviewViewModel = await _reviewService.GetReviewByIdAsync(reviewId);

            return Ok(reviewViewModel);
        }

        /// <summary>
        /// Creates a new review
        /// </summary>
        /// <param name="reviewInputModel"></param>
        /// <returns></returns>
        /// Sample request:
        ///     POST 
        /// 
        ///     {
        ///         "rating": 5,
        ///         "comment": "Highly recommended",
        ///         "productId": 1,
        ///         "userId": 3
        ///     }
        /// </remarks>
        /// <response code="200">Returns when review is successfully created</response>
        /// <response code="400">Returns when review input details are incorrect</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> CreateAsync([FromBody] ReviewInputModel reviewInputModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _reviewService.CreateReviewAsync(reviewInputModel);

            return Ok();
        }

        /// <summary>
        /// Updates an existing review
        /// </summary>
        /// <param name="reviewId"></param>
        /// <param name="reviewInputModel"></param>
        /// <returns></returns>
        /// Sample request:
        ///     PUT 
        /// 
        ///     {
        ///         "rating": 5,
        ///         "comment": "Highly recommended",
        ///         "productId": 1,
        ///         "userId": 3
        ///     }
        /// </remarks>
        /// <response code="200">Returns when review is successfully updated</response>
        /// <response code="400">Returns when review input details are incorrect</response>
        [HttpPut("{reviewId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> UpdateAsync([FromRoute] int reviewId, [FromBody] ReviewInputModel reviewInputModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _reviewService.UpdateReviewAsync(reviewId, reviewInputModel);

            return Ok();
        }

        /// <summary>
        /// Deletes an existing review
        /// </summary>
        /// <param name="reviewId"></param>
        /// <returns></returns>
        /// <response code="200">Returns when review is successfully deleted</response>
        [HttpDelete("{reviewId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> RemoveAsync([FromRoute] int reviewId)
        {
            await _reviewService.RemoveReviewAsync(reviewId);

            return Ok();
        }
    }
}
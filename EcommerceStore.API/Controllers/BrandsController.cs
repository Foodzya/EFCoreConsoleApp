using EcommerceStore.API.Authentication;
using EcommerceStore.API.Constants;
using EcommerceStore.Application.Exceptions;
using EcommerceStore.Application.Interfaces;
using EcommerceStore.Application.Models.InputModels;
using EcommerceStore.Application.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EcommerceStore.API.Controllers
{
    /// <summary>
    /// Brand controller for managing brands using CRUD operations
    /// </summary>
    [ApiController]
    [Route("api/brands")]
    public class BrandsController : ControllerBase
    {
        private readonly IBrandService _brandService;

        public BrandsController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        /// <summary>
        /// Get brand by brand id
        /// </summary>
        /// <param name="brandId"></param>
        /// <returns></returns>
        /// <response code="200">Returns when brand is successfully obtained</response>
        [Authorize(Policy = AuthPolicies.CustomerAccess)]
        [HttpGet("{brandId}")]
        [ProducesResponseType(typeof(BrandViewModel), StatusCodes.Status200OK)]
        public async Task<ActionResult<BrandViewModel>> GetByIdAsync([FromRoute] int brandId)
        {
            BrandViewModel brandViewModel = await _brandService.GetBrandByIdAsync(brandId);

            return Ok(brandViewModel);
        }

        /// <summary>
        /// Get list of brands
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Returns when list of brands is successfully obtained</response>
        [Authorize(Policy = AuthPolicies.CustomerAccess)]
        [HttpGet]
        [ProducesResponseType(typeof(List<BrandViewModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<BrandViewModel>>> GetAllAsync()
        {
            var brandsViewModels = await _brandService.GetAllBrandsAsync();

            return Ok(brandsViewModels);
        }

        /// <summary>
        /// Deletes brand by brand id
        /// </summary>
        /// <param name="brandId"></param>
        /// <returns></returns>
        /// <response code="200">Returns when brand is successfully deleted</response>
        [Authorize(Policy = AuthPolicies.AdminAccess)]
        [HttpDelete("{brandId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> DeleteByIdAsync([FromRoute] int brandId)
        {
            await _brandService.RemoveBrandByIdAsync(brandId);
            
            return Ok();
        }

        /// <summary>
        /// Creates a brand 
        /// </summary>
        /// <param name="brandInputModel"></param>
        /// <remarks>
        /// Sample request:
        ///     POST 
        /// 
        ///     {
        ///         "name": "Adidas",
        ///         "foundationYear": 1990
        ///     }
        /// </remarks>
        /// <response code="200">Returns when brand is successfully created</response>
        /// <response code="400">Returns when input brand details are incorrect</response>
        [Authorize(Policy = AuthPolicies.AdminAccess)]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> CreateAsync([FromBody] BrandInputModel brandInputModel)
        {
            if (!ModelState.IsValid)
                throw new ValidationException(ModelState);

            await _brandService.CreateBrandAsync(brandInputModel);

            return Ok();
        }

        /// <summary>
        /// Updates an existing brand by brand id
        /// </summary>
        /// <param name="brandId"></param>
        /// <param name="brandInputModel"></param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        ///     PUT 
        /// 
        ///     {
        ///         "name": "Nike",
        ///         "foundationYear": 1949
        ///     }
        /// </remarks>
        /// <response code="200">Returns when brand is successfully updated</response>
        /// <response code="400">Returns when input brand details are incorrect</response>
        [Authorize(Policy = AuthPolicies.AdminAccess)]
        [HttpPut("{brandId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> UpdateAsync([FromRoute] int brandId, [FromBody] BrandInputModel brandInputModel)
        {
            if (!ModelState.IsValid)
                throw new ValidationException(ModelState);

            await _brandService.UpdateBrandAsync(brandId, brandInputModel);

            return Ok();
        }
    }
}
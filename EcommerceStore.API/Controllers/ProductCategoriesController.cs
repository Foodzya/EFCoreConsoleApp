using System.Collections.Generic;
using System.Threading.Tasks;
using EcommerceStore.Application.Exceptions;
using EcommerceStore.Application.Interfaces;
using EcommerceStore.Application.Models.InputModels;
using EcommerceStore.Application.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceStore.API.Controllers
{
    /// <summary>
    /// ProductCategory controller for managing product categories using CRUD operations
    /// </summary>
    [ApiController]
    [Route("api/product-categories")]
    public class ProductCategoriesController : ControllerBase
    {
        private readonly IProductCategoryService _productCategoryService;
        private readonly ISectionService _sectionService;

        public ProductCategoriesController(IProductCategoryService productCategoryService, ISectionService sectionService)
        {
            _productCategoryService = productCategoryService;
            _sectionService = sectionService;
        }

        /// <summary>
        /// Get list of product categories
        /// </summary>
        /// <param name="sectionId"></param>
        /// <returns></returns>
        /// <response code="200">Returns when list of product categories is successfully obtained</response>
        [HttpGet("/section/{sectionId}")]
        [ProducesResponseType(typeof(List<ProductCategoryViewModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<ProductCategoryViewModel>>> GetAllAsync([FromBody] int sectionId)
        {
            var productCategoriesViewModel = await _productCategoryService.GetAllProductCategoriesForSectionAsync(sectionId);

            return Ok(productCategoriesViewModel);
        }

        /// <summary>
        /// Get single product category
        /// </summary>
        /// <param name="productCategoryId"></param>
        /// <returns></returns>
        /// <response code="200">Returns when product category is successfully obtained</response>
        [HttpGet("{productCategoryId}")]
        [ProducesResponseType(typeof(ProductCategoryViewModel), StatusCodes.Status200OK)]
        public async Task<ActionResult<ProductCategoryViewModel>> GetByIdAsync([FromRoute] int productCategoryId)
        {
            var productCategoryViewModel = await _productCategoryService.GetProductCategoryByIdAsync(productCategoryId);

            return Ok(productCategoryViewModel);
        }

        /// <summary>
        /// Update an existing product category
        /// </summary>
        /// <param name="productCategoryId"></param>
        /// <param name="productCategoryIm"></param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        ///     PUT 
        /// 
        ///     {
        ///         "name": "Sneakers",
        ///         "parentCategoryId": 1,
        ///         "sectionId": 2
        ///     }
        /// </remarks>
        /// <response code="200">Returns when product category is successfully updated</response>
        /// <response code="400">Returns when product category input details are incorrect</response>
        [HttpPut("{productCategoryId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> UpdateAsync([FromRoute] int productCategoryId, [FromBody] ProductCategoryInputModel productCategoryIm)
        {
            if (!ModelState.IsValid)
                throw new ValidationException(ModelState);

            await _productCategoryService.UpdateProductCategoryAsync(productCategoryId, productCategoryIm);

            return Ok();
        }

        /// <summary>
        /// Creates new product category
        /// </summary>
        /// <param name="productCategoryIm"></param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        ///     POST 
        /// 
        ///     {
        ///         "name": "Sneakers",
        ///         "parentCategoryId": 1,
        ///         "sectionId": 1
        ///     }
        /// </remarks>
        /// <response code="200">Returns when order is successfully created</response>
        /// <response code="400">Returns when order input details are incorrect</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> CreateAsync([FromBody] ProductCategoryInputModel productCategoryIm)
        {
            if (!ModelState.IsValid)
                throw new ValidationException(ModelState);

            await _productCategoryService.CreateProductCategoryAsync(productCategoryIm);

            return Ok();
        }

        /// <summary>
        /// Deletes product category
        /// </summary>
        /// <param name="productCategoryId"></param>
        /// <returns></returns>
        /// <response code="200">Returns when order is successfully deleted</response>
        [HttpDelete("{productCategoryId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> DeleteByIdAsync([FromRoute] int productCategoryId)
        {
            await _productCategoryService.RemoveProductCategoryAsync(productCategoryId);

            return Ok();
        }

        /// <summary>
        /// Links product category to section
        /// </summary>
        /// <param name="productCategoryId"></param>
        /// <param name="sectionId"></param>
        /// <returns></returns>
        /// <response code="200">Returns when product category is successfully attached to section</response>
        [HttpPut("{productCategoryId}/sections/{sectionId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> LinkCategoryToSectionAsync([FromRoute] int productCategoryId, [FromRoute] int sectionId)
        {
            await _sectionService.LinkCategoryToSectionAsync(productCategoryId, sectionId);

            return Ok();
        }
    }
}
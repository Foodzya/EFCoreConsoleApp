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
    /// Product controller for managing products using CRUD operations
    /// </summary>
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Get list of products
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Returns when list of products is successfully obtained</response>
        [Authorize(Roles = $"{Roles.admin},{Roles.customer}")]
        [HttpGet]
        [ProducesResponseType(typeof(List<ProductViewModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<ProductViewModel>>> GetAllAsync()
        {
            var productsViewModel = await _productService.GetAllProductsAsync();

            return Ok(productsViewModel);
        }

        /// <summary>
        /// Get single product
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        /// <response code="200">Returns when product is successfully obtained</response>
        [Authorize(Roles = $"{Roles.admin},{Roles.customer}")]
        [HttpGet("{productId}")]
        public async Task<ActionResult<ProductViewModel>> GetByIdAsync([FromRoute] int productId)
        {
            var productViewModel = await _productService.GetProductByIdAsync(productId);

            return Ok(productViewModel);
        }

        /// <summary>
        /// Updates an existing product
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="productIm"></param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        ///     PUT 
        /// 
        ///     {
        ///         "name": "iPhone 11",
        ///         "price": 1000,
        ///         "quantity": 20,
        ///         "description": "Nice smartphone",
        ///         "image": "https://linktoimage.com/",
        ///         "brandId": 3,
        ///         "productCategoryId": 2
        ///     }
        /// </remarks>
        /// <response code="200">Returns when product is successfully updated</response>
        /// <response code="400">Returns when product input details are incorrect</response>
        [Authorize(Roles = Roles.admin)]
        [HttpPut("{productId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> UpdateAsync([FromRoute] int productId, [FromBody] ProductInputModel productIm)
        {
            if (!ModelState.IsValid)
                throw new ValidationException(ModelState);

            await _productService.UpdateProductAsync(productId, productIm);

            return Ok();
        }

        /// <summary>
        /// Creates a new product
        /// </summary>
        /// <param name="productIm"></param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        ///     POST 
        /// 
        ///     {
        ///         "name": "iPhone 11",
        ///         "price": 1000,
        ///         "quantity": 20,
        ///         "description": "Nice smartphone",
        ///         "image": "https://linktoimage.com/",
        ///         "brandId": 3,
        ///         "productCategoryId": 2
        ///     }
        /// </remarks>
        /// <response code="200">Returns when product is successfully created</response>
        /// <response code="400">Returns when product input details are incorrect</response>
        [Authorize(Roles = Roles.admin)]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> CreateAsync([FromBody] ProductInputModel productIm)
        {
            if (!ModelState.IsValid)
                throw new ValidationException(ModelState);

            await _productService.CreateProductAsync(productIm);

            return Ok();
        }

        /// <summary>
        /// Deletes an existing product
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        /// <response code="200">Returns when product is successfully deleted</response>
        [Authorize(Roles = Roles.admin)]
        [HttpDelete("{productId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> DeleteByIdAsync([FromRoute] int productId)
        {
            await _productService.RemoveProductByIdAsync(productId);

            return Ok();
        }
    }
}
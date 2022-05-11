using System.Collections.Generic;
using System.Threading.Tasks;
using EcommerceStore.Application.Exceptions;
using EcommerceStore.Application.Interfaces;
using EcommerceStore.Application.Models.InputModels;
using EcommerceStore.Application.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceStore.API.Controllers
{
    [ApiController]
    [Route("api/productcategories")]
    public class ProductCategoriesController : ControllerBase
    {
        private readonly IProductCategoryService _productCategoryService;

        public ProductCategoriesController(IProductCategoryService productCategoryService)
        {
            _productCategoryService = productCategoryService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductCategoryViewModel>>> GetAllAsync()
        {
            var productCategoriesViewModel = await _productCategoryService.GetAllProductCategoriesAsync();

            return Ok(productCategoriesViewModel);
        }

        [HttpGet("{productCategoryId}")]
        public async Task<ActionResult<ProductCategoryViewModel>> GetByIdAsync([FromRoute] int productCategoryId)
        {
            var productCategoryViewModel = await _productCategoryService.GetProductCategoryByIdAsync(productCategoryId);

            return Ok(productCategoryViewModel);
        }

        [HttpPut("{productCategoryId}")]
        public async Task<ActionResult> UpdateAsync([FromRoute] int productCategoryId, [FromBody] ProductCategoryInputModel productCategoryIm)
        {
            if (!ModelState.IsValid)
                throw new ValidationException(ModelState);

            await _productCategoryService.UpdateProductCategoryAsync(productCategoryId, productCategoryIm);

            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult> CreateAsync([FromBody] ProductCategoryInputModel productCategoryIm)
        {
            if (!ModelState.IsValid)
                throw new ValidationException(ModelState);

            await _productCategoryService.CreateProductCategoryAsync(productCategoryIm);

            return Ok();
        }

        [HttpDelete("{productCategoryId}")]
        public async Task<ActionResult> DeleteByIdAsync([FromRoute] int productCategoryId)
        {
            await _productCategoryService.RemoveProductCategoryAsync(productCategoryId);

            return Ok();
        }
    }
}
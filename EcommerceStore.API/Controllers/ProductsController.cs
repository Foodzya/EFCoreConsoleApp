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
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductViewModel>>> GetAllAsync()
        {
            var productsViewModel = await _productService.GetAllProductsAsync();

            return Ok(productsViewModel);
        }

        [HttpGet("{productId}")]
        public async Task<ActionResult<ProductViewModel>> GetByIdAsync([FromRoute] int productId)
        {
            var productViewModel = await _productService.GetProductByIdAsync(productId);

            return Ok(productViewModel);
        }

        [HttpPut("{productId}")]
        public async Task<ActionResult> UpdateAsync([FromRoute] int productId, [FromBody] ProductInputModel productIm)
        {
            if(!ModelState.IsValid)
                throw new ValidationException(ModelState);

            await _productService.UpdateProductAsync(productId, productIm);

            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult> CreateAsync([FromBody] ProductInputModel productIm)
        {
            if (!ModelState.IsValid)
                throw new ValidationException(ModelState);

            await _productService.CreateProductAsync(productIm);

            return Ok();
        }

        [HttpDelete("{productId}")]
        public async Task<ActionResult> DeleteByIdAsync([FromRoute] int productId)
        {
            await _productService.RemoveProductByIdAsync(productId);

            return Ok();
        }
    }
}
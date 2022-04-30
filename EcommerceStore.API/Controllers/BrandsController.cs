using EcommerceStore.Application.Interfaces;
using EcommerceStore.Application.Models.InputModels;
using EcommerceStore.Application.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EcommerceStore.API.Controllers
{
    [ApiController]
    [Route("api/brands")]
    public class BrandsController : ControllerBase
    {
        private readonly IBrandService _brandService;

        public BrandsController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        [HttpGet("{brandId}")]
        public async Task<ActionResult<BrandViewModel>> GetByIdAsync([FromRoute] int brandId)
        {
            BrandViewModel brandViewModel = await _brandService.GetBrandByIdAsync(brandId);

            if (brandViewModel == null)
            {
                return NotFound();
            }

            return Ok(brandViewModel);
        }

        [HttpGet]
        public async Task<ActionResult<List<BrandViewModel>>> GetAllAsync()
        {
            var brandsViewModels = await _brandService.GetAllBrandsAsync();

            if (brandsViewModels == null)
            {
                return NotFound();
            }

            return Ok(brandsViewModels);
        }

        [HttpDelete("{brandId}")]
        public async Task<ActionResult> DeleteByIdAsync([FromRoute] int brandId)
        {
            await _brandService.RemoveBrandByIdAsync(brandId);
            
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult> AddAsync([FromBody] BrandInputModel brandInputModel)
        {
            if (!ModelState.IsValid)
            {
                return NotFound();
            }

            await _brandService.CreateBrandAsync(brandInputModel);

            return Ok();
        }

        [HttpPut("{brandId}")]
        public async Task<ActionResult> ModifyAsync([FromRoute] int brandId, [FromBody] BrandInputModel brandInputModel)
        {
            if (!ModelState.IsValid)
            {
                return NotFound();
            }

            await _brandService.UpdateBrandAsync(brandId, brandInputModel);

            return Ok();
        }
    }
}
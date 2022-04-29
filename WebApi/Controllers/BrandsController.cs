using EcommerceStore.Application.Interfaces.Services;
using EcommerceStore.Infrastucture.Persistence.Models.InputModels;
using EcommerceStore.Infrastucture.Persistence.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EcommerceStore.WebApi.Controllers
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
            BrandViewModel brandViewModel = await _brandService.GetByIdAsync(brandId);

            if (brandViewModel == null)
            {
                return NotFound();
            }

            return Ok(brandViewModel);
        }

        [HttpGet]
        public async Task<ActionResult<List<BrandViewModel>>> GetAllAsync()
        {
            var brandsViewModels = await _brandService.GetAllAsync();

            if (brandsViewModels == null)
            {
                return NotFound();
            }

            return Ok(brandsViewModels);
        }

        [HttpDelete("{brandId}")]
        public async Task<ActionResult> DeleteByIdAsync([FromRoute] int brandId)
        {
            await _brandService.RemoveByIdAsync(brandId);
            
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult> AddAsync([FromBody] BrandInputModel brandInputModel)
        {
            if (!ModelState.IsValid)
            {
                return NotFound();
            }

            await _brandService.AddAsync(brandInputModel);

            return Ok();
        }

        [HttpPut("{brandId}")]
        public async Task<ActionResult> ModifyAsync([FromRoute] int brandId, [FromBody] BrandInputModel brandInputModel)
        {
            if (!ModelState.IsValid)
            {
                return NotFound();
            }

            await _brandService.ModifyAsync(brandId, brandInputModel);

            return Ok();
        }
    }
}
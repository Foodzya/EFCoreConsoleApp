using EcommerceStore.Controllers.InputModels;
using EcommerceStore.Controllers.ViewModels;
using EcommerceStore.Data.Context;
using EcommerceStore.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceStore.Controllers
{
    [ApiController]
    [Route("api/brands")]
    public class BrandsController : ControllerBase
    {
        private readonly EcommerceContext _context;

        public BrandsController(EcommerceContext context)
        {
            _context = context;
        }

        [HttpGet("{brandId}")]
        public async Task<ActionResult<BrandViewModel>> GetByIdAsync([FromRoute] int brandId)
        {
            var brandViewModel = await _context.Brands
                .Where(b => b.Id == brandId)
                .Select(b => new BrandViewModel
                {
                    Name = b.Name,
                    FoundationYear = b.FoundationYear,
                    ProductsCount = b.Products.Count()
                })
                .FirstOrDefaultAsync();

            if (brandViewModel == null)
            {
                return NotFound();
            }

            return Ok(brandViewModel);
        }

        [HttpGet]
        public async Task<ActionResult<List<BrandViewModel>>> GetAllAsync()
        {
            var brandsViewModel = await _context.Brands
                .Select(b => new BrandViewModel
                {
                    Name = b.Name,
                    FoundationYear = b.FoundationYear,
                    ProductsCount = b.Products.Count()
                })
                .ToListAsync();

            if (brandsViewModel == null)
            {
                return NotFound();
            }

            return Ok(brandsViewModel);
        }

        [HttpDelete("{brandId}")]
        public async Task<ActionResult> DeleteByIdAsync([FromRoute] int brandId)
        {
            var brand = await _context.Brands.FirstOrDefaultAsync(b => b.Id == brandId);

            if (brand == null || brand.IsDeleted)
            {
                return NotFound();
            }

            brand.IsDeleted = true;

            await _context.SaveChangesAsync();

            return Ok();

        }

        [HttpPost]
        public async Task<ActionResult> AddAsync([FromBody] BrandInputModel brandInputModel)
        {
            if (!ModelState.IsValid)
            {
                return NotFound();
            }

            var brand = new Brand
            {
                Name = brandInputModel.Name,
                FoundationYear = brandInputModel.FoundationYear
            };

            await _context.Brands.AddAsync(brand);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("{brandId}")]
        public async Task<ActionResult> ModifyAsync([FromRoute] int brandId, [FromBody] BrandInputModel brandInputModel)
        {
            if (!ModelState.IsValid)
            {
                return NotFound();
            }

            var brand = await _context.Brands.FirstOrDefaultAsync(b => b.Id == brandId);

            brand.Name = brandInputModel.Name;
            brand.FoundationYear = brandInputModel.FoundationYear;

            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
using EcommerceStore.Controllers.ExtensionMappers;
using EcommerceStore.Controllers.InputModels;
using EcommerceStore.Controllers.ViewModels;
using EcommerceStore.Data.Context;
using EcommerceStore.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

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
        public ActionResult<BrandViewModel> GetById([FromRoute] int brandId)
        {
            var brand = _context.Brands
                .Include(b => b.Products)
                .FirstOrDefault(b => b.Id == brandId);

            if (brand != null)
            {
                var brandView = brand.MapToBrandView();

                return Ok(brandView);
            }
            else
                return NotFound();
        }

        [HttpGet]
        public ActionResult<List<BrandViewModel>> GetAll()
        {
            List<BrandViewModel> viewBrands = new List<BrandViewModel>();

            var brands = _context.Brands
                .Include(b => b.Products)
                .ToList();

            foreach (var b in brands)
            {
                var viewBrand = b.MapToBrandView();

                viewBrands.Add(viewBrand);
            }

            if (viewBrands != null)
            {
                return Ok(viewBrands);
            }

            return NotFound();

        }

        [HttpDelete("{brandId}")]
        public ActionResult DeleteById([FromRoute] int brandId)
        {
            var brand = _context.Brands.FirstOrDefault(b => b.Id == brandId);

            if (brand != null)
            {
                _context.Brands.Remove(brand);
                _context.SaveChanges();

                return Ok();
            }

            return NotFound();
        }

        [HttpPost]
        public ActionResult Add([FromBody] BrandInputModel inputModel)
        {
            if (inputModel != null)
            {
                var brand = inputModel.MapToBrand();

                _context.Brands.Add(brand);

                _context.SaveChanges();

                return Ok();
            }            

            return NotFound();
        }

        [HttpPut("{brandId}")]
        public ActionResult Modify([FromRoute] int brandId, [FromBody] Brand newBrand)
        {
            var brand = _context.Brands.FirstOrDefault(b => b.Id == brandId);

            if (brand != null)
            {
                brand.Name = newBrand.Name;
                brand.FoundationYear = newBrand.FoundationYear;

                _context.SaveChanges();

                return Ok();
            }

            return NotFound();
        }
    }
}
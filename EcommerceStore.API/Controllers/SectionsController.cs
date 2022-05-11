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
    [Route("api/sections")]
    public class SectionsController : ControllerBase
    {
        private readonly ISectionService _sectionService;

        public SectionsController(ISectionService sectionService)
        {
            _sectionService = sectionService;
        }

        [HttpGet]
        public async Task<ActionResult<List<SectionViewModel>>> GetAllAsync()
        {
            var sectionsViewModel = await _sectionService.GetAllSectionsAsync();

            return Ok(sectionsViewModel);
        }

        [HttpGet("{sectionId}")]
        public async Task<ActionResult<SectionViewModel>> GetByIdAsync([FromRoute] int sectionId)
        {
            var sectionViewModel = await _sectionService.GetSectionByIdAsync(sectionId);

            return Ok(sectionViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> CreateAsync([FromBody] SectionInputModel sectionInputModel)
        {
            if (!ModelState.IsValid)
                throw new ValidationException(ModelState);

            await _sectionService.CreateSectionAsync(sectionInputModel);

            return Ok();
        }

        [HttpPut("{sectionId}")]
        public async Task<ActionResult> UpdateAsync([FromRoute] int sectionId, SectionInputModel sectionInputModel)
        {
            if (!ModelState.IsValid)
                throw new ValidationException(ModelState);

            await _sectionService.UpdateSectionAsync(sectionId, sectionInputModel);

            return Ok();
        }

        [HttpDelete("{sectionId}")]
        public async Task<ActionResult> RemoveAsync([FromRoute] int sectionId)
        {
            await _sectionService.RemoveSectionAsync(sectionId);

            return Ok();
        }
    }
}
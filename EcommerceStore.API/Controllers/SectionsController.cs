using System.Collections.Generic;
using System.Threading.Tasks;
using EcommerceStore.API.Authentication;
using EcommerceStore.API.Constants;
using EcommerceStore.Application.Exceptions;
using EcommerceStore.Application.Interfaces;
using EcommerceStore.Application.Models.InputModels;
using EcommerceStore.Application.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceStore.API.Controllers
{
    /// <summary>
    /// Section controller for managing sections using CRUD operations
    /// </summary>
    [ApiController]
    [Route("api/sections")]
    public class SectionsController : ControllerBase
    {
        private readonly ISectionService _sectionService;

        public SectionsController(ISectionService sectionService)
        {
            _sectionService = sectionService;
        }

        /// <summary>
        /// Get a list of sections
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Returns when list of sections is successfully obtained</response>
        [Authorize(Policy = AuthPolicies.CustomerAccess)]
        [HttpGet]
        [ProducesResponseType(typeof(List<SectionViewModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<SectionViewModel>>> GetAllAsync()
        {
            var sectionsViewModel = await _sectionService.GetAllSectionsAsync();

            return Ok(sectionsViewModel);
        }

        /// <summary>
        /// Get an existing section
        /// </summary>
        /// <param name="sectionId"></param>
        /// <returns></returns>
        /// <response code="200">Returns when list of sections is successfully obtained</response>
        [Authorize(Policy = AuthPolicies.CustomerAccess)]
        [HttpGet("{sectionId}")]
        [ProducesResponseType(typeof(SectionViewModel), StatusCodes.Status200OK)]
        public async Task<ActionResult<SectionViewModel>> GetByIdAsync([FromRoute] int sectionId)
        {
            var sectionViewModel = await _sectionService.GetSectionByIdAsync(sectionId);

            return Ok(sectionViewModel);
        }

        /// <summary>
        /// Creates a new section
        /// </summary>
        /// <param name="sectionInputModel"></param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        ///     POST 
        /// 
        ///     {
        ///         "name": "Kids"
        ///     }
        /// </remarks>
        /// <response code="200">Returns when section is successfully created</response>
        /// <response code="400">Returns when section input details are incorrect</response>
        [Authorize(Policy = AuthPolicies.AdminAccess)]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> CreateAsync([FromBody] SectionInputModel sectionInputModel)
        {
            if (!ModelState.IsValid)
                throw new ValidationException(ModelState);

            await _sectionService.CreateSectionAsync(sectionInputModel);

            return Ok();
        }

        /// <summary>
        /// Updates an existing section
        /// </summary>
        /// <param name="sectionId"></param>
        /// <param name="sectionInputModel"></param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        ///     PUT 
        /// 
        ///     {
        ///         "name": "Kids new"
        ///     }
        /// </remarks>
        /// <response code="200">Returns when section is successfully renamed</response>
        /// <response code="400">Returns when section input name is incorrect</response>
        [Authorize(Policy = AuthPolicies.AdminAccess)]
        [HttpPut("{sectionId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> RenameAsync([FromRoute] int sectionId, SectionInputModel sectionInputModel)
        {
            if (!ModelState.IsValid)
                throw new ValidationException(ModelState);

            await _sectionService.RenameSectionAsync(sectionId, sectionInputModel);

            return Ok();
        }

        /// <summary>
        /// Deletes an existing section
        /// </summary>
        /// <param name="sectionId"></param>
        /// <returns></returns>
        /// <response code="200">Returns when section is successfully deleted</response>
        [Authorize(Policy = AuthPolicies.AdminAccess)]
        [HttpDelete("{sectionId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> RemoveAsync([FromRoute] int sectionId)
        {
            await _sectionService.RemoveSectionAsync(sectionId);

            return Ok();
        }
    }
}
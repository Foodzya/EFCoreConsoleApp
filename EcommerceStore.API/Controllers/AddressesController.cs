﻿using EcommerceStore.Application.Interfaces;
using EcommerceStore.Application.Models.InputModels;
using EcommerceStore.Application.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EcommerceStore.API.Controllers
{
    /// <summary>
    /// Address controller for managing addresses using CRUD operations
    /// </summary>
    [ApiController]
    [Route("api/addresses")]
    public class AddressesController : ControllerBase
    {
        private readonly IAddressService _addressService;

        public AddressesController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        /// <summary>
        /// Get address by specific ID 
        /// </summary>
        /// <param name="addressId"></param>
        /// <returns></returns>
        /// <response code="200">When address is successfully obtained</response>
        [HttpGet("{addressId}")]
        [ProducesResponseType(typeof(AddressViewModel), StatusCodes.Status200OK)]
        public async Task<ActionResult<AddressViewModel>> GetByIdAsync([FromRoute] int addressId)
        {
            AddressViewModel addressViewModel = await _addressService.GetAddressByIdAsync(addressId);

            return Ok(addressViewModel);
        }
        
        /// <summary>
        /// Get all existing addresses
        /// </summary>
        /// <returns></returns>
        /// <response code="200">When list of addresses is successfully obtained</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<AddressViewModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<AddressViewModel>>> GetAllAsync()
        {
            var addressesViewModel = await _addressService.GetAllAddressesAsync();

            return Ok(addressesViewModel);
        }

        /// <summary>
        /// Deletes the address by specific ID
        /// </summary>
        /// <param name="addressId"></param>
        /// <returns></returns>
        [HttpDelete("{addressId}")]
        public async Task<ActionResult> DeleteByIdAsync([FromRoute] int addressId)
        {
            await _addressService.RemoveAddressByIdAsync(addressId);

            return Ok();
        }

        /// <summary>
        /// Creates an address
        /// </summary>
        /// <param name="addressInputModel"></param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        ///     POST 
        /// 
        ///     {
        ///         "streetAddress": "some address details",
        ///         "postcode": 123456,
        ///         "city": "some city",
        ///         "userId": 1
        ///     }
        /// </remarks>
        /// <response code="200">Returns when address is successfully created</response>
        /// <response code="400">If the address is null</response>
        [HttpPost]
        [ProducesResponseType(typeof(AddressViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> CreateAsync([FromBody] AddressInputModel addressInputModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _addressService.CreateAddressAsync(addressInputModel);

            return Ok();
        }

        /// <summary>
        /// Updates an existing address
        /// </summary>
        /// <param name="addressId"></param>
        /// <param name="addressInputModel"></param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        ///     POST 
        /// 
        ///     {
        ///         "streetAddress": "some address details",
        ///         "postcode": 123456,
        ///         "city": "some city",
        ///         "userId": 1
        ///     }
        /// </remarks>
        /// <response code="200">Returns when address is successfully updated</response>
        /// <response code="400">If the input address is null or incorrect</response>
        [HttpPut("{addressId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> UpdateAsync([FromRoute] int addressId, [FromBody] AddressInputModel addressInputModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _addressService.UpdateAddressAsync(addressId, addressInputModel);

            return Ok();
        }
    }
}
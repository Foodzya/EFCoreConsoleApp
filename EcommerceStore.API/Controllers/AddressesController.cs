using EcommerceStore.Application.Exceptions;
using EcommerceStore.Application.Interfaces;
using EcommerceStore.Application.Models.InputModels;
using EcommerceStore.Application.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EcommerceStore.API.Controllers
{
    /// <summary>
    /// Address controller for managing Addresses using CRUD operations
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
        [HttpGet("{addressId}")]
        public async Task<ActionResult<AddressViewModel>> GetByIdAsync([FromRoute] int addressId)
        {
            AddressViewModel addressViewModel = await _addressService.GetAddressByIdAsync(addressId);

            return Ok(addressViewModel);
        }
        
        /// <summary>
        /// Get all existing addresses
        /// </summary>
        /// <returns></returns>
        [HttpGet]
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
        /// POST /addresses
        /// {
        ///     "streetAddress": "some address details",
        ///     "postcode": 123456,
        ///     "city": "some city",
        ///     "userId": 1
        /// }
        /// </remarks>
        /// <exception cref="ValidationException"></exception>
        [HttpPost]
        public async Task<ActionResult> CreateAsync([FromBody] AddressInputModel addressInputModel)
        {
            if (!ModelState.IsValid)
                throw new ValidationException(ModelState);

            await _addressService.CreateAddressAsync(addressInputModel);

            return Ok();
        }

        /// <summary>
        /// Updates an existing address
        /// </summary>
        /// <param name="addressId"></param>
        /// <param name="addressInputModel"></param>
        /// <returns></returns>
        /// <exception cref="ValidationException"></exception>
        [HttpPut("{addressId}")]
        public async Task<ActionResult> UpdateAsync([FromRoute] int addressId, [FromBody] AddressInputModel addressInputModel)
        {
            if (!ModelState.IsValid)
            {
                throw new ValidationException(ModelState);
            }

            await _addressService.UpdateAddressAsync(addressId, addressInputModel);

            return Ok();
        }
    }
}
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
    [Route("api/addresses")]
    public class AddressesController : ControllerBase
    {
        private readonly IAddressService _addressService;

        public AddressesController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        [HttpGet("{addressId}")]
        public async Task<ActionResult<AddressViewModel>> GetByIdAsync([FromRoute] int addressId)
        {
            AddressViewModel addressViewModel = await _addressService.GetAddressByIdAsync(addressId);

            return Ok(addressViewModel);
        }

        [HttpGet]
        public async Task<ActionResult<List<AddressViewModel>>> GetAllAsync()
        {
            var addressesViewModel = await _addressService.GetAllAddressesAsync();

            return Ok(addressesViewModel);
        }

        [HttpDelete("{addressId}")]
        public async Task<ActionResult> DeleteByIdAsync([FromRoute] int addressId)
        {
            await _addressService.RemoveAddressByIdAsync(addressId);

            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult> CreateAsync([FromBody] AddressInputModel addressInputModel)
        {
            if (!ModelState.IsValid)
                throw new ValidationException(ModelState);

            await _addressService.CreateAddressAsync(addressInputModel);

            return Ok();
        }

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
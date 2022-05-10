using EcommerceStore.Application.Models.InputModels;
using EcommerceStore.Application.Models.ViewModels;

namespace EcommerceStore.Application.Interfaces
{
    public interface IAddressService
    {
        public Task<List<AddressViewModel>> GetAllAddressesAsync();
        public Task<AddressViewModel> GetAddressByIdAsync(int addressId);
        public Task RemoveAddressByIdAsync(int addressId);
        public Task CreateAddressAsync(AddressInputModel addressInputModel);
        public Task UpdateAddressAsync(int addressId, AddressInputModel addressInputModel);
    }
}
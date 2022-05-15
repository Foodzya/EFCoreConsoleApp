using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcommerceStore.Application.Exceptions;
using EcommerceStore.Application.Interfaces;
using EcommerceStore.Application.Models.InputModels;
using EcommerceStore.Application.Models.ViewModels;
using EcommerceStore.Domain.Entities;
using EcommerceStore.Domain.Interfaces;

namespace EcommerceStore.Application.Services
{
    public class AddressService : IAddressService
    {
        private readonly IAddressRepository _addressRepository;

        public AddressService(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }

        public async Task CreateAddressAsync(int userId, AddressInputModel addressInputModel)
        {
            var address = new Address
            {
                StreetAddress = addressInputModel.StreetAddress,
                City = addressInputModel.City,
                PostCode = addressInputModel.PostCode,
                UserId = userId
            };

            await _addressRepository.CreateAsync(address);

            await _addressRepository.SaveChangesAsync();
        }

        public async Task<AddressViewModel> GetAddressByIdAsync(int addressId)
        {
            var address = await _addressRepository.GetByIdAsync(addressId);

            if (address is null)
                throw new ValidationException(NotFoundExceptionMessages.AddressNotFound, addressId);

            var addressViewModel = new AddressViewModel
            {
                Id = address.Id,
                City = address.City,
                PostCode = address.PostCode,
                StreetAddress = address.StreetAddress,
                UserFullName = $"{address.User.FirstName} {address.User.LastName}"
            };

            return addressViewModel;
        }
        public async Task<List<AddressViewModel>> GetAllAddressesForUserAsync(int userId)
        {
            var addresses = await _addressRepository.GetAllForUserAsync(userId);

            if (addresses is null)
                throw new ValidationException(NotFoundExceptionMessages.AddressNotFound);

            var addressesViewModel = addresses
                .Select(a => new AddressViewModel
                {
                    Id = a.Id,
                    City = a.City,
                    PostCode = a.PostCode,
                    StreetAddress = a.StreetAddress,
                    UserFullName = $"{a.User.FirstName} {a.User.LastName}"
                })
                .ToList();

            return addressesViewModel;
        }

        public async Task RemoveAddressByIdAsync(int addressId)
        {
            var address = await _addressRepository.GetByIdAsync(addressId);

            if (address is null)
                throw new ValidationException(NotFoundExceptionMessages.AddressNotFound, addressId);

            _addressRepository.Remove(address);

            await _addressRepository.SaveChangesAsync();
        }

        public async Task UpdateAddressAsync(int addressId, AddressInputModel addressInputModel)
        {
            var address = await _addressRepository.GetByIdAsync(addressId);

            if (address is null)
                throw new ValidationException(NotFoundExceptionMessages.AddressNotFound, addressId);

            address.PostCode = addressInputModel.PostCode;
            address.City = addressInputModel.City;
            address.StreetAddress = addressInputModel.StreetAddress;

            _addressRepository.Update(address);

            await _addressRepository.SaveChangesAsync();
        }
    }
}
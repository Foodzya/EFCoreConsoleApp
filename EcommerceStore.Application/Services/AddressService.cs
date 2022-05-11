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

        public async Task CreateAddressAsync(AddressInputModel addressInputModel)
        {
            var address = new Address
            {
                StreetAddress = addressInputModel.StreetAddress,
                City = addressInputModel.City,
                PostCode = addressInputModel.PostCode,
                UserId = addressInputModel.UserId
            };

            await _addressRepository.CreateAsync(address);

            await _addressRepository.SaveChangesAsync();
        }

        public async Task<AddressViewModel> GetAddressByIdAsync(int addressId)
        {
            var address = await _addressRepository.GetByIdAsync(addressId);

            if (address is null)
                throw new ValidationException(ExceptionMessages.AddressNotFound, addressId);

            var addressViewModel = new AddressViewModel
            {
                City = address.City,
                PostCode = address.PostCode,
                StreetAddress = address.StreetAddress,
                UserFullName = $"{address.User.FirstName} + \" \" + {address.User.LastName}"
            };

            return addressViewModel;
        }
        public async Task<List<AddressViewModel>> GetAllAddressesAsync()
        {
            var addresses = await _addressRepository.GetAllAsync();

            if (addresses is null)
                throw new ValidationException(ExceptionMessages.AddressNotFound);

            var addressesViewModel = addresses
                .Select(a => new AddressViewModel
                {
                    City = a.City,
                    PostCode = a.PostCode,
                    StreetAddress = a.StreetAddress,
                    UserFullName = $"{a.User.FirstName} + \" \" + {a.User.LastName}"
                })
                .ToList();

            return addressesViewModel;
        }

        public async Task RemoveAddressByIdAsync(int addressId)
        {
            var address = await _addressRepository.GetByIdAsync(addressId);

            if (address is null)
                throw new ValidationException(ExceptionMessages.AddressNotFound, addressId);

            _addressRepository.Remove(address);

            await _addressRepository.SaveChangesAsync();
        }

        public async Task UpdateAddressAsync(int addressId, AddressInputModel addressInputModel)
        {
            var address = await _addressRepository.GetByIdAsync(addressId);

            if (address is null)
                throw new ValidationException(ExceptionMessages.AddressNotFound, addressId);

            address.PostCode = addressInputModel.PostCode;
            address.City = addressInputModel.City;
            address.StreetAddress = addressInputModel.StreetAddress;
            address.UserId = addressInputModel.UserId;

            _addressRepository.Update(address);

            await _addressRepository.SaveChangesAsync();
        }
    }
}
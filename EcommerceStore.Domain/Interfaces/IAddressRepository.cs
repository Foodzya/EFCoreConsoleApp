using System.Collections.Generic;
using System.Threading.Tasks;
using EcommerceStore.Domain.Entities;

namespace EcommerceStore.Domain.Interfaces
{
    public interface IAddressRepository
    {
        public Task<Address> GetByIdAsync(int addressId);
        public Task<List<Address>> GetAllAsync();
        public Task CreateAsync(Address address);
        public void Remove(Address address);
        public void Update(Address address);
        public Task SaveChangesAsync();
    }
}